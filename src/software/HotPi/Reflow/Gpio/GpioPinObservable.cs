using System;
using Raspberry.IO.GeneralPurpose;

namespace Restall.HotPi.Reflow.Gpio
{
	public class GpioPinObservable : IDisposable
	{
		private readonly GpioConnection gpio;
		private readonly IObserve<GpioPinChanged> observer;
		private readonly Func<DateTime> now;

		public GpioPinObservable(GpioConnection gpio, IObserve<GpioPinChanged> observer, Func<DateTime> now)
		{
			this.gpio = gpio;
			this.observer = observer;
			this.now = now;
			this.gpio.PinStatusChanged += this.OnPinStatusChanged;
		}

		private void OnPinStatusChanged(object sender, PinStatusEventArgs args)
		{
			this.observer.Observe(new GpioPinChanged(
				this.now(),
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
