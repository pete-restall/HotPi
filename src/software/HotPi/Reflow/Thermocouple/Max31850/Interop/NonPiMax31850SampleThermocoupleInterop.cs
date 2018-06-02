using System;
using System.Diagnostics;
using System.Threading;

namespace Restall.HotPi.Reflow.Thermocouple.Max31850.Interop
{
	public class NonPiMax31850SampleThermocoupleInterop : IMax31850SampleThermocoupleInterop
	{
		private readonly Func<DateTime> now;

		public NonPiMax31850SampleThermocoupleInterop(Func<DateTime> now)
		{
			this.now = now;
		}

		public Max31850SampleThermocoupleResponse SampleThermocouple()
		{
			Debug.WriteLine("[{0}] [{1}] Max31850SampleThermocoupleInterop.SampleThermocouple()", this.now(), Thread.CurrentThread.ManagedThreadId);
			return new Max31850SampleThermocoupleResponse(presencePulse: false, conversionTimeout: false, timestamp: DateTime.Now);
		}
	}
}
