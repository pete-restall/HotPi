namespace Restall.HotPi.Reflow.Thermocouple.Max31850
{
	public class Max31850JThermocoupleSampleToVoltageMapper : IMapper<ThermocoupleSample, Voltage>
	{
		private const decimal LinearCoefficientJ = 57.953e-6m;

		public Voltage Map(ThermocoupleSample obj)
		{
			var uncompensatedTemperature = obj.ThermocoupleTemperature.Kelvin - obj.ColdJunctionTemperature.Kelvin;
			return new Voltage(uncompensatedTemperature * LinearCoefficientJ);
		}
	}
}
