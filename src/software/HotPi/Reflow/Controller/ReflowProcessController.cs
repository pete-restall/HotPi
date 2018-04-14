using System;
using System.Linq;
using NullGuard;
using Restall.HotPi.Reflow.Profiles;

namespace Restall.HotPi.Reflow.Controller
{
	public class ReflowProcessController : IControlReflowProcess, IProvideReflowTemperatureSetpoints, IObserve<PidControllerAdjusted>
	{
		private interface IRunningState
		{
			IRunningState CloneWith(ReflowProcessContext context);

			StartProcessRunCommand Running { get; }

			Tuple<Temperature, bool> GetNextSetpoint();
		}

		private class StoppedState : IRunningState
		{
			public IRunningState CloneWith(ReflowProcessContext context)
			{
				return this;
			}

			[AllowNull]
			public StartProcessRunCommand Running => null;

			public Tuple<Temperature, bool> GetNextSetpoint()
			{
				return Tuple.Create(Temperature.Undefined, true);
			}
		}

		private class RunningState : IRunningState
		{
			private readonly ReflowZone[] zones;
			private readonly ReflowProcessContext context;

			public RunningState(StartProcessRunCommand running, ReflowZone[] zones, [AllowNull] ReflowProcessContext context)
			{
				this.Running = running;
				this.zones = zones;
				this.context = context;
			}

			public StartProcessRunCommand Running { get; }

			public Tuple<Temperature, bool> GetNextSetpoint()
			{
				if (this.context == null)
					return Tuple.Create(Temperature.Undefined, false);

				var zone = this.zones.FirstOrDefault(x => x.CanProvideNextSetpoint(this.context));
				if (zone == null)
					return Tuple.Create(Temperature.Undefined, true);

				return Tuple.Create(zone.GetNextSetpoint(this.context), false);
			}

			public IRunningState CloneWith([AllowNull] ReflowProcessContext context)
			{
				return new RunningState(this.Running, this.zones, context);
			}
		}

		private static readonly StoppedState Stopped = new StoppedState();

		private readonly object startSync = new object();
		private readonly IHaveReflowTimebaseSettings settings;

		private IRunningState runningState;
		private ReflowProcessContext context;

		public ReflowProcessController(IHaveReflowTimebaseSettings settings)
		{
			this.settings = settings;
			this.runningState = Stopped;
		}

		bool IControlReflowProcess.TryStart(StartProcessRunCommand profile, out StartProcessRunCommand running)
		{
			lock (this.startSync)
			{
				var state = this.runningState;
				if (state.Running != null)
				{
					running = state.Running;
					return false;
				}

				this.runningState = new RunningState(profile, profile.Zones.ToArray(), this.context);
				running = profile;
				return true;
			}
		}

		void IControlReflowProcess.Abort()
		{
			this.runningState = Stopped;
		}

		Temperature IProvideReflowTemperatureSetpoints.GetNextSetpoint()
		{
			var state = this.runningState;
			if (state == null)
				return Temperature.Undefined;

			var setpoint = state.GetNextSetpoint();
			this.runningState = setpoint.Item2 ? Stopped : this.runningState.CloneWith(this.context);

			return setpoint.Item1;
		}

		void IObserve<PidControllerAdjusted>.Observe(PidControllerAdjusted observed)
		{
			this.context = new ReflowProcessContext(this.settings.SamplingInterval, observed);
		}
	}
}
