using System;

namespace Restall.HotPi.Reflow
{
	public class Tolerance
	{
		private readonly double lowerFraction;
		private readonly double upperFraction;

		public Tolerance(double percent)
			: this(percent, percent)
		{
		}

		public Tolerance(double lowerPercent, double upperPercent)
		{
			if (lowerPercent < 0)
				throw new ArgumentOutOfRangeException(nameof(lowerPercent), lowerPercent, "Tolerance percentage should be a positive number");

			if (upperPercent < 0)
				throw new ArgumentOutOfRangeException(nameof(upperPercent), upperPercent, "Tolerance percentage should be a positive number");

			this.lowerFraction = lowerPercent / 100;
			this.upperFraction = upperPercent / 100;
		}

		public ToleranceCheck<T> ForTarget<T>(T target, Func<T, double> getValue)
		{
			double targetValue = getValue(target);
			return new ToleranceCheck<T>(
				targetValue * (1 - this.lowerFraction),
				targetValue * (1 + this.upperFraction),
				getValue);
		}
	}
}
