using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Restall.HotPi.Reflow.Thermocouple.Max31850
{
	[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local", Justification = CodeAnalysisJustification.ForSerialisation)]
	public class Max31850ReadScratchpadResponse
	{
		public Max31850ReadScratchpadResponse(bool presencePulse, IEnumerable<byte> raw)
		{
			var scratchpad = raw.ToArray();
			var dataCorrupted = IsSerialTransmissionCorrupted(scratchpad);

			this.PresencePulse = presencePulse;

			this.Raw = scratchpad;

			this.ThermocoupleConversionFaults = new ThermocoupleConversionFaults(
				noDevicePresent: !presencePulse,
				serialTransmissionCorrupted: dataCorrupted,
				unknownFault: !dataCorrupted && scratchpad.IsBitSet(0, 0),
				shortedToVdd: !dataCorrupted && scratchpad.IsBitSet(2, 2),
				shortedToVss: !dataCorrupted && scratchpad.IsBitSet(2, 1),
				openCircuit: !dataCorrupted && scratchpad.IsBitSet(2, 0));

			this.ColdJunctionTemperature = dataCorrupted
				? Temperature.Undefined
				: DecodeColdJunctionTemperature(scratchpad);

			this.ThermocoupleTemperature = dataCorrupted
				? Temperature.Undefined
				: DecodeThermocoupleTemperature(scratchpad);
		}

		private static bool IsSerialTransmissionCorrupted(IReadOnlyCollection<byte> bytes)
		{
			return bytes.Count != 9 || !bytes.IsMax31850CrcValid();
		}

		private static Temperature DecodeColdJunctionTemperature(IReadOnlyList<byte> scratchpad)
		{
			const decimal toDecimal = 1m / (1 << 24);
			int fixedPoint = (scratchpad[3] << 24) | ((scratchpad[2] & 0xf0) << 16);
			return (fixedPoint * toDecimal).Celsius();
		}

		private static Temperature DecodeThermocoupleTemperature(IReadOnlyList<byte> scratchpad)
		{
			const decimal toDecimal = 1m / (1 << 20);
			int fixedPoint = (scratchpad[1] << 24) | ((scratchpad[0] & 0xfc) << 16);
			return (fixedPoint * toDecimal).Celsius();
		}

		public bool PresencePulse { get; private set; }

		public IEnumerable<byte> Raw { get; private set; }

		public ThermocoupleConversionFaults ThermocoupleConversionFaults { get; private set; }

		public Temperature ColdJunctionTemperature { get; private set; }

		public Temperature ThermocoupleTemperature { get; private set; }
	}
}
