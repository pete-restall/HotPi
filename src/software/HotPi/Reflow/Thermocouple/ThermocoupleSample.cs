using System;
using System.Diagnostics.CodeAnalysis;

namespace Restall.HotPi.Reflow.Thermocouple
{
	[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local", Justification = CodeAnalysisJustification.ForSerialisation)]
	public class ThermocoupleSample
	{
		public ThermocoupleSample(
			DateTime timestamp,
			ThermocoupleConversionFaults conversionFaults,
			Temperature coldJunctionTemperature,
			Temperature thermocoupleTemperature)
		{
			this.Timestamp = timestamp;
			this.ConversionFaults = conversionFaults;
			this.ColdJunctionTemperature = coldJunctionTemperature;
			this.ThermocoupleTemperature = thermocoupleTemperature;
		}

		public DateTime Timestamp { get; private set; }

		public ThermocoupleConversionFaults ConversionFaults { get; private set; }

		public Temperature ColdJunctionTemperature { get; private set; }

		public Temperature ThermocoupleTemperature { get; private set; }
	}
}
