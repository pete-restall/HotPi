using System;
using System.Threading.Tasks;

namespace Restall.HotPi.Reflow
{
	public class DummyReflowPlant : IHostReflowPlant
	{
		private readonly IObserve<OvenTemperatureSampled> observer;

		private Task task;
		private bool stop;

		public DummyReflowPlant(IObserve<OvenTemperatureSampled> observer)
		{
			this.observer = observer;
		}

		public void Start()
		{
			this.stop = false;
			this.task = Task.Run(this.RandomTemperatureReadings);
		}

		private async Task RandomTemperatureReadings()
		{
			var random = new Random();
			while (!this.stop)
			{
				this.observer.Observe(new OvenTemperatureSampled(DateTime.Now, new Temperature((decimal) random.NextDouble() * 500)));
				await Task.Delay(TimeSpan.FromMilliseconds(500));
			}
		}

		public void Stop()
		{
			this.stop = true;
		}
	}
}
