using System;
using System.Diagnostics;
using System.Threading;

namespace Restall.HotPi.Reflow.Thermocouple.Max31850.Interop
{
	public class NonPiMax31850SampleThermocoupleInterop : IMax31850SampleThermocoupleInterop
	{
		public Max31850SampleThermocoupleResponse SampleThermocouple()
		{
			Debug.WriteLine("[{0}] [{1}] Max31850SampleThermocoupleInterop.SampleThermocouple()", DateTime.Now, Thread.CurrentThread.ManagedThreadId);
			return new Max31850SampleThermocoupleResponse(presencePulse: false, conversionTimeout: false, timestamp: DateTime.Now);
		}
	}
}
