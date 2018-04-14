namespace Restall.HotPi.Reflow
{
	public static class IntegerTemperatureExtensions
	{
		public static Temperature Celsius(this int value)
		{
			return ((decimal) value).Celsius();
		}
	}
}
