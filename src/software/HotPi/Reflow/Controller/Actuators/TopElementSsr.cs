using System;
using Raspberry.IO;
using Restall.HotPi.Reflow.Gpio;

namespace Restall.HotPi.Reflow.Controller.Actuators
{
	public class TopElementSsr : IObserve<PidControllerAdjusted>
	{
		private readonly IOutputBinaryPin ssr;
		private readonly Watchdog.Watchdog wdt;

		public TopElementSsr([GpioPin("SSR1")] IOutputBinaryPin ssr, Watchdog.Watchdog wdt)
		{
			this.ssr = ssr;
			this.wdt = wdt;
		}

		public void Observe(PidControllerAdjusted observed)
		{
			if (observed.SampleInterval == TimeSpan.Zero)
				return;

			if (observed.ProcessVariable >= 270.Celsius())
			{
				Console.WriteLine("OVER TEMPERATURE CONDITION AT {0}K !  TOP SSR TURNED OFF, WDT NOT CLEARED", observed.ProcessVariable.Kelvin);
				this.ssr.Write(false);
				return;
			}

			Console.WriteLine("TOP SSR {0} - SP {1}, PV {2}, CV {3}", observed.ControlVariable > 0 ? "ON" :"OFF", observed.Setpoint.Kelvin, observed.ProcessVariable, observed.ControlVariable);
			this.ssr.Write(observed.ControlVariable > 0);
			this.wdt.Clear();
		}
	}
}
