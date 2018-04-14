using System;

namespace Restall.HotPi.Reflow
{
	public static class IntegerTimeSpanExtensions
	{
		public static TimeSpan Seconds(this int value)
		{
			return TimeSpan.FromSeconds(value);
		}

		public static TimeSpan Milliseconds(this int value)
		{
			return TimeSpan.FromMilliseconds(value);
		}
	}
}
