using System;
using System.Runtime.InteropServices;

namespace Restall.HotPi.Reflow.Thermocouple.Max31850.Interop
{
	public class Max31850SampleThermocoupleInterop : IMax31850SampleThermocoupleInterop
	{
		public Max31850SampleThermocoupleResponse SampleThermocouple()
		{
			var now = DateTime.Now;
			int result = Max31850SampleThermocouple();
			return new Max31850SampleThermocoupleResponse(
				presencePulse: Max31850ErrorInterop.IsPresencePulseError(result),
				conversionTimeout: IsConversionTimeoutError(result),
				timestamp: now);
		}

		[DllImport(Max31850Interop.NativeLibraryName, EntryPoint = "max31850SampleThermocouple")]
		private static extern int Max31850SampleThermocouple();

		private static bool IsConversionTimeoutError(int result)
		{
			return Max31850ErrorInterop.IsError(result, 32);
		}
	}
}
