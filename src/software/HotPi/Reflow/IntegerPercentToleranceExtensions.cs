namespace Restall.HotPi.Reflow
{
	public static class IntegerPercentToleranceExtensions
	{
		public static PercentTolerance PercentTolerance(this int value)
		{
			return new PercentTolerance(value);
		}
	}
}
