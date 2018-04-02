using System;
using Raspberry.IO.GeneralPurpose;
using Restall.HotPi.Reflow.Gpio;

namespace Restall.HotPi.Reflow.Watchdog
{
	public class WatchdogChangedPinObservable : IObserve<GpioPinChanged>
	{
		private static readonly ProcessorPin WatchdogExpiredPin = ConnectorPin.P1Pin19.ToProcessor();

		private readonly IObserve<WatchdogChanged> observer;
		private readonly Func<DateTime> now;

		public WatchdogChangedPinObservable(IObserve<WatchdogChanged> observer, Func<DateTime> now)
		{
			this.observer = observer;
			this.now = now;
		}

		void IObserve<GpioPinChanged>.Observe(GpioPinChanged observed)
		{
			if (observed.Pin != WatchdogExpiredPin)
				return;

			this.observer.Observe(new WatchdogChanged(this.now(), observed.Active));
		}
	}
}
