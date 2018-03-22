using System.Collections.Generic;
using System.Linq;

namespace Restall.HotPi.Reflow.Thermocouple.Max31850
{
	public static class EnumerableOfByteMax31850CrcExtensions
	{
		public static bool IsMax31850CrcValid(this IEnumerable<byte> source)
		{
			var shiftRegister = new bool[8];
			foreach (var bit in source.ToBits())
			{
				var newEntry = bit ^ shiftRegister[7];
				shiftRegister[7] = shiftRegister[6];
				shiftRegister[6] = shiftRegister[5];
				shiftRegister[5] = shiftRegister[4] ^ newEntry;
				shiftRegister[4] = shiftRegister[3] ^ newEntry;
				shiftRegister[3] = shiftRegister[2];
				shiftRegister[2] = shiftRegister[1];
				shiftRegister[1] = shiftRegister[0];
				shiftRegister[0] = newEntry;
			}

			return shiftRegister.All(x => x == false);
		}

		private static IEnumerable<bool> ToBits(this IEnumerable<byte> source)
		{
			return source.SelectMany(x => x.ToBitsLsbFirst());
		}
	}
}
