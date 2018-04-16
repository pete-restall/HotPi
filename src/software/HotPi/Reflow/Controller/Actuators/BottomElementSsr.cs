using System;
using Raspberry.IO;
using Restall.HotPi.Reflow.Gpio;

namespace Restall.HotPi.Reflow.Controller.Actuators
{
	public class BottomElementSsr : IObserve<PidControllerAdjusted>
	{
		private readonly IOutputBinaryPin ssr;
		private readonly Watchdog.Watchdog wdt;

		public BottomElementSsr([GpioPin("SSR1")] IOutputBinaryPin ssr, Watchdog.Watchdog wdt)
		{
			this.ssr = ssr;
			this.wdt = wdt;
		}

		public void Observe(PidControllerAdjusted observed)
		{
			if (observed.SampleInterval == TimeSpan.Zero)
				return;

			Console.WriteLine("BOTTOM SSR {0} - SP {1}, PV {2}, CV {3}", observed.ControlVariable > 0 ? "ON" : "OFF", observed.Setpoint.Kelvin, observed.ProcessVariable, observed.ControlVariable);
			this.ssr.Write(observed.ControlVariable > 0);
			this.wdt.Clear();
		}
	}
}
