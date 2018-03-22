using System;
using Raspberry.IO.GeneralPurpose;
using Restall.HotPi.Reflow.Gpio;

namespace Restall.HotPi.Reflow.Watchdog
{
	public class WatchdogTimerExpiredPinObservable : IObserve<GpioPinChanged>
	{
		private static readonly ProcessorPin WatchdogExpiredPin = ConnectorPin.P1Pin19.ToProcessor();

		private readonly IObserve<WatchdogTimerExpired> observer;

		public WatchdogTimerExpiredPinObservable(IObserve<WatchdogTimerExpired> observer)
		{
			this.observer = observer;
		}

		void IObserve<GpioPinChanged>.Observe(GpioPinChanged observed)
		{
			if (observed.Pin != WatchdogExpiredPin)
				return;

			this.observer.Observe(new WatchdogTimerExpired(DateTime.Now));
		}
	}
}
