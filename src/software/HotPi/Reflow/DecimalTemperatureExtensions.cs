namespace Restall.HotPi.Reflow
{
	public static class DecimalTemperatureExtensions
	{
		public static Temperature Celsius(this decimal value)
		{
			return new Temperature(value + 273.15m);
		}
	}
}
