using System;
using System.Diagnostics.CodeAnalysis;

namespace Restall.HotPi.Reflow.Controller
{
	[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local", Justification = CodeAnalysisJustification.ForSerialisation)]
	public class PidControllerAdjusted
	{
		public PidControllerAdjusted(
			DateTime timestamp,
			DateTime sampleTimestamp,
			TimeSpan sampleInterval,
			double integral,
			double derivative,
			Temperature setpoint,
			Temperature processVariable,
			double controlVariable)
		{
			this.Timestamp = timestamp;
			this.SampleTimestamp = sampleTimestamp;
			this.SampleInterval = sampleInterval;
			this.Integral = integral;
			this.Derivative = derivative;
			this.Setpoint = setpoint;
			this.ProcessVariable = processVariable;
			this.ControlVariable = controlVariable;
		}

		public DateTime Timestamp { get; private set; }

		public DateTime SampleTimestamp { get; private set; }

		public TimeSpan SampleInterval { get; private set; }

		public double Integral { get; private set; }

		public double Derivative { get; private set; }

		public Temperature Setpoint { get; private set; }

		public Temperature ProcessVariable { get; private set; }

		public double ControlVariable { get; private set; }
	}
}
