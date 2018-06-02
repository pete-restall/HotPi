using System;
using Raspberry.Timers;

namespace Restall.HotPi.Reflow.Thermocouple
{
	public class ThermocoupleMonitoringProcess : IReflowPlantProcess
	{
		private readonly IHaveReflowTimebaseSettings settings;
		private readonly ITimer timer;
		private readonly ThermocoupleSampler sampler;
		private readonly IObserve<ThermocoupleSampled> observer;
		private readonly Func<DateTime> now;

		public ThermocoupleMonitoringProcess(
			IHaveReflowTimebaseSettings settings,
			ITimer timer,
			ThermocoupleSampler sampler,
			IObserve<ThermocoupleSampled> observer,
			Func<DateTime> now)
		{
			this.settings = settings;
			this.timer = timer;
			this.sampler = sampler;
			this.observer = observer;
			this.now = now;
		}

		public void Start()
		{
			this.timer.Interval = this.settings.SamplingInterval;
			this.timer.Action = this.SampleThermocouple;
			this.timer.Start(5.Seconds());
		}

		private void SampleThermocouple()
		{
			try
			{
				var timestamp = this.now();
				var sample = this.sampler.Read();
				this.observer.Observe(new ThermocoupleSampled(timestamp, sample));
			}
			catch
			{
				// Thread entry point - do not allow to leak
				// TODO: logging ???
			}
		}

		public void Stop()
		{
			this.timer.Stop();
		}
	}
}
