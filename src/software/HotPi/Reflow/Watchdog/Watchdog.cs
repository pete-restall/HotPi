using System.Diagnostics;
using Raspberry.IO;
using Restall.HotPi.Reflow.Gpio;

namespace Restall.HotPi.Reflow.Watchdog
{
	public class Watchdog
	{
		private readonly IOutputBinaryPin clrwdt;

		public Watchdog([GpioPin("CLRWDT")] IOutputBinaryPin clrwdt)
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
