namespace Restall.HotPi.Reflow.Thermocouple.Max31850
{
	public class Max31850ColdJunctionErrorCompensation : IMapper<Temperature, Temperature>
	{
		private readonly decimal staticTemperatureOffset;

		public Max31850ColdJunctionErrorCompensation(IHaveMax31850Settings settings)
		{
			this.staticTemperatureOffset = settings.ColdJunctionStaticErrorInCelsius;
		}

		public Temperature Map(Temperature obj)
		{
			// TODO: FROM THE DATASHEET THE ERROR CURVE LOOKS LIKE A QUADRATIC - FIGURE OUT THE COEFFICIENTS AND APPLY THE CORRECTION
			return obj != Temperature.Undefined
				? new Temperature(obj.Kelvin - this.staticTemperatureOffset)
				: Temperature.Undefined;
		}
	}
}
