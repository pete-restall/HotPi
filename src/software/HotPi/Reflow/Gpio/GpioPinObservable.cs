using System;
using Raspberry.IO.GeneralPurpose;

namespace Restall.HotPi.Reflow.Gpio
{
	public class GpioPinObservable : IDisposable
	{
		private readonly GpioConnection gpio;
		private readonly IObserve<GpioPinChanged> observer;

		public GpioPinObservable(GpioConnection gpio, IObserve<GpioPinChanged> observer)
		{
			this.gpio = gpio;
			this.observer = observer;
			this.gpio.PinStatusChanged += this.OnPinStatusChanged;
		}

		private void OnPinStatusChanged(object sender, PinStatusEventArgs args)
		{
			this.observer.Observe(new GpioPinChanged(
				DateTime.Now,
				args.Configuration.Pin,
				args.Configuration.Reversed ? !args.Enabled : args.Enabled));
		}

		public void Dispose()
		{
			this.gpio.PinStatusChanged -= this.OnPinStatusChanged;
			GC.SuppressFinalize(this);
		}
	}
}
