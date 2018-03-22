using System.Diagnostics;
using Raspberry.IO.GeneralPurpose;
using Restall.HotPi.Reflow.Gpio;

namespace Restall.HotPi.Reflow.Watchdog
{
	public class Watchdog
	{
		private readonly GpioOutputBinaryPin clrwdt;

		public Watchdog([GpioPin("CLRWDT")] GpioOutputBinaryPin clrwdt)
		{
			this.clrwdt = clrwdt;
		}

		public void Clear()
		{
			this.clrwdt.Write(true);
			WaitAtLeast150Nanoseconds();

			this.clrwdt.Write(false);
			WaitAtLeast150Nanoseconds();
		}

		private static void WaitAtLeast150Nanoseconds()
		{
			var stopwatch = Stopwatch.StartNew();
			while (stopwatch.ElapsedTicks < 2)
				;
		}
	}
}
