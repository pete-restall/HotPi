using System;
using System.Diagnostics.CodeAnalysis;

namespace Restall.HotPi.Reflow.Thermocouple
{
	[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local", Justification = CodeAnalysisJustification.ForSerialisation)]
	public class ThermocoupleSampled
	{
		public ThermocoupleSampled(DateTime timestamp, ThermocoupleSample sample)
		{
			this.Timestamp = timestamp;
			this.Sample = sample;
		}

		public DateTime Timestamp { get; private set; }

		public ThermocoupleSample Sample { get; private set; }
	}
}
