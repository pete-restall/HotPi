using System;
using Raspberry.Timers;

namespace Restall.HotPi.Reflow.Thermocouple
{
	public class ThermocoupleMonitoringProcess : IReflowPlantProcess
	{
		private readonly ITimer timer;
		private readonly ThermocoupleSampler sampler;
		private readonly IObserve<ThermocoupleSampled> observer;

		public ThermocoupleMonitoringProcess(ITimer timer, ThermocoupleSampler sampler, IObserve<ThermocoupleSampled> observer)
		{
			this.timer = timer;
			this.sampler = sampler;
			this.observer = observer;
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
				var timestamp = DateTime.Now;
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
