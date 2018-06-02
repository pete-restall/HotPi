using System;
using Restall.HotPi.Reflow.Thermocouple.Max31850;

namespace Restall.HotPi.Reflow.Thermocouple
{
	public class ThermocoupleSampler
	{
		private readonly IExecuteMax31850CommandsInNearRealtime max31850;
		private readonly IMax31850SampleThermocoupleInterop sampleInterop;
		private readonly IMax31850ReadScratchpadInterop scratchpadInterop;
		private readonly Func<DateTime> now;

		public ThermocoupleSampler(
			IExecuteMax31850CommandsInNearRealtime max31850,
			IMax31850SampleThermocoupleInterop sampleInterop,
			IMax31850ReadScratchpadInterop scratchpadInterop,
			Func<DateTime> now)
		{
			this.max31850 = max31850;
			this.sampleInterop = sampleInterop;
			this.scratchpadInterop = scratchpadInterop;
			this.now = now;
		}

		public ThermocoupleSample Read()
		{
			try
			{
				return this.ReadWithPossibleThrow();
			}
			catch (TimeoutException)
			{
				return new ThermocoupleSample(
					this.now(),
					new ThermocoupleConversionFaults(timeout: true),
					Temperature.Undefined,
					Temperature.Undefined);
			}
		}

		private ThermocoupleSample ReadWithPossibleThrow()
		{
			var conversionResponse = this.max31850.Execute(this.sampleInterop.SampleThermocouple, 200.Milliseconds());
			if (!conversionResponse.PresencePulse)
			{
				return new ThermocoupleSample(
					conversionResponse.Timestamp,
					new ThermocoupleConversionFaults(noDevicePresent: true),
					Temperature.Undefined,
					Temperature.Undefined);
			}

			var scratchpad = this.max31850.Execute(this.scratchpadInterop.ReadScratchpad, 200.Milliseconds());
			return new ThermocoupleSample(
				conversionResponse.Timestamp,
				scratchpad.ThermocoupleConversionFaults,
				scratchpad.ColdJunctionTemperature,
				scratchpad.ThermocoupleTemperature);
		}
	}
}
