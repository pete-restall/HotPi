using System.Collections.Generic;

namespace Restall.HotPi.Reflow.Thermocouple.Max31850
{
	public static class ByteBitExtensions
	{
		public static bool IsBitSet(this byte[] source, int offset, int flag)
		{
			return source[offset].IsBitSet(flag);
		}

		private static bool IsBitSet(this byte source, int flag)
		{
			return (source & (0x01 << flag)) != 0;
		}

		public static IEnumerable<bool> ToBitsLsbFirst(this byte source)
		{
			for (int i = 0; i < 8; i++)
				yield return (source & (0x01 << i)) != 0;
		}
	}
}
