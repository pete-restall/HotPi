using System;
using System.Runtime.InteropServices;

namespace Restall.HotPi.Reflow.Thermocouple.Max31850.Interop
{
	public class Max31850SampleThermocoupleInterop : IMax31850SampleThermocoupleInterop
	{
		private readonly Func<DateTime> now;

		public Max31850SampleThermocoupleInterop(Func<DateTime> now)
		{
			this.now = now;
		}

		public Max31850SampleThermocoupleResponse SampleThermocouple()
		{
			var timestamp = this.now();
			int result = Max31850SampleThermocouple();
			return new Max31850SampleThermocoupleResponse(
				presencePulse: !Max31850ErrorInterop.IsPresencePulseError(result),
				conversionTimeout: IsConversionTimeoutError(result),
				timestamp: timestamp);
		}

		[DllImport(Max31850Interop.NativeLibraryName, EntryPoint = "max31850SampleThermocouple", CallingConvention = CallingConvention.Cdecl)]
		private static extern int Max31850SampleThermocouple();

		private static bool IsConversionTimeoutError(int result)
		{
			return Max31850ErrorInterop.IsError(result, 32);
		}
	}
}
