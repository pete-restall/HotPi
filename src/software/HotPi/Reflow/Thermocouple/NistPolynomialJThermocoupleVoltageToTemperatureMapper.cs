namespace Restall.HotPi.Reflow.Thermocouple
{
	public class NistPolynomialJThermocoupleVoltageToTemperatureMapper : IMapper<Voltage, Temperature>
	{
		private static readonly decimal[] Coefficients =
		{
			+0.000000000000e+00m,
			+0.503811878150e-01m,
			+0.304758369300e-04m,
			-0.856810657200e-07m,
			+0.132281952950e-09m,
			-0.170529583370e-12m,
			+0.209480906970e-15m,
			-0.125383953360e-18m,
			+0.156317256970e-22m
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
