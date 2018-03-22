using System.Diagnostics.CodeAnalysis;

namespace Restall.HotPi.Reflow.Thermocouple
{
	[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local", Justification = CodeAnalysisJustification.ForSerialisation)]
	public class ThermocoupleConversionFaults
	{
		public ThermocoupleConversionFaults(
			bool timeout = false,
			bool noDevicePresent = false,
			bool unknownFault = false,
			bool serialTransmissionCorrupted = false,
			bool shortedToVdd = false,
			bool shortedToVss = false,
			bool openCircuit = false)
		{
			this.NoDevicePresent = noDevicePresent;
			this.UnknownFault = unknownFault;
			this.SerialTransmissionCorrupted = serialTransmissionCorrupted;
			this.ShortedToVdd = shortedToVdd;
			this.ShortedToVss = shortedToVss;
			this.OpenCircuit = openCircuit;
		}

		public bool Timeout { get; private set; }

		public bool NoDevicePresent { get; private set; }

		public bool UnknownFault { get; private set; }

		public bool SerialTransmissionCorrupted { get; private set; }

		public bool ShortedToVdd { get; private set; }

		public bool ShortedToVss { get; private set; }

		public bool OpenCircuit { get; private set; }

		public bool AnyFault =>
			this.Timeout ||
			this.NoDevicePresent ||
			this.UnknownFault ||
			this.SerialTransmissionCorrupted ||
			this.ShortedToVdd ||
			this.ShortedToVss ||
			this.OpenCircuit;
	}
}
