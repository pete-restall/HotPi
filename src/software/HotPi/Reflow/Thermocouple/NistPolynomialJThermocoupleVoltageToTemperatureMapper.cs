namespace Restall.HotPi.Reflow.Thermocouple
{
	public class NistPolynomialJThermocoupleVoltageToTemperatureMapper : IMapper<Voltage, Temperature>
	{
		private static readonly decimal[] Coefficients =
		{
			+0.000000e+00m,
			+1.978425e+01m,
			-2.001204e-01m,
			+1.036969e-02m,
			-2.549687e-04m,
			+3.585153e-06m,
			-5.344285e-08m,
			+5.099890e-10m,
			+0.000000e+00m
		};

		public Temperature Map(Voltage obj)
		{
			var temperature = Coefficients[0];
			var millivolts = obj.Millivolts;
			var millivoltsRaisedToPowerI = millivolts;
			for (int i = 1; i < Coefficients.Length; i++)
			{
				temperature += Coefficients[i] * millivoltsRaisedToPowerI;
				millivoltsRaisedToPowerI *= millivolts;
			}

			return temperature.Celsius();
		}
	}
}
