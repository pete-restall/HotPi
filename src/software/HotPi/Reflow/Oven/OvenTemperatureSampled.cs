using System;
using System.Diagnostics.CodeAnalysis;

namespace Restall.HotPi.Reflow.Oven
{
	public class OvenTemperatureSampled
	{
		public OvenTemperatureSampled(DateTime timestamp, Temperature temperature)
		{
			this.Timestamp = timestamp;
			this.Temperature = temperature;
		}

		[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local", Justification = CodeAnalysisJustification.ForSerialisation)]
		public DateTime Timestamp { get; private set; }

		[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local", Justification = CodeAnalysisJustification.ForSerialisation)]
		public Temperature Temperature { get; private set; }
	}
}
