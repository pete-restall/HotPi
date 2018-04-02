using System;
using Raspberry.Timers;

namespace Restall.HotPi.Reflow.Thermocouple
{
	public class ThermocoupleMonitoringProcess : IReflowPlantProcess
	{
		private readonly ITimer timer;
		private readonly ThermocoupleSampler sampler;
		private readonly IObserve<ThermocoupleSampled> observer;
		private readonly Func<DateTime> now;

		public ThermocoupleMonitoringProcess(
			ITimer timer,
			ThermocoupleSampler sampler,
			IObserve<ThermocoupleSampled> observer,
			Func<DateTime> now)
		{
			this.timer = timer;
			this.sampler = sampler;
			this.observer = observer;
			this.now = now;
		}

		public void Start()
		{
			this.timer.Interval = TimeSpan.FromSeconds(1);
			this.timer.Action = this.SampleThermocouple;
			this.timer.Start(TimeSpan.FromSeconds(5));
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
