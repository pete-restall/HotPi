using System;
using System.Diagnostics.CodeAnalysis;
using Raspberry.IO.GeneralPurpose;

namespace Restall.HotPi.Reflow.Gpio
{
	[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local", Justification = CodeAnalysisJustification.ForSerialisation)]
	public class GpioPinChanged
	{
		public GpioPinChanged(DateTime timestamp, ProcessorPin pin, bool active)
		{
			this.Timestamp = timestamp;
			this.Pin = pin;
			this.Active = active;
		}

		public DateTime Timestamp { get; private set; }

		public ProcessorPin Pin { get; private set; }

		public bool Active { get; private set; }
	}
}
