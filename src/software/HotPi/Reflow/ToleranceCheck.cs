using System;

namespace Restall.HotPi.Reflow
{
	public class ToleranceCheck<T>
	{
		private readonly double minimum;
		private readonly double maximum;
		private readonly Func<T, double> getValue;

		public ToleranceCheck(double minimum, double maximum, Func<T, double> getValue)
		{
			this.minimum = minimum;
			this.maximum = maximum;
			this.getValue = getValue;
		}

		public bool IsWithinTolerance(T value)
		{
			double toCheck = this.getValue(value);
			return toCheck >= this.minimum && toCheck <= this.maximum;
		}
	}
}
