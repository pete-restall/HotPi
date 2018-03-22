using System;
using System.Diagnostics.CodeAnalysis;

namespace Restall.HotPi.Reflow.Thermocouple.Max31850
{
	[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local", Justification = CodeAnalysisJustification.ForSerialisation)]
	public class Max31850SampleThermocoupleResponse
	{
		public Max31850SampleThermocoupleResponse(bool presencePulse, bool conversionTimeout, DateTime timestamp)
		{
			this.PresencePulse = presencePulse;
			this.ConversionTimeout = conversionTimeout;
			this.Timestamp = timestamp;
		}

		public bool PresencePulse { get; private set; }

		public bool ConversionTimeout { get; private set; }

		public DateTime Timestamp { get; private set; }
	}
}
