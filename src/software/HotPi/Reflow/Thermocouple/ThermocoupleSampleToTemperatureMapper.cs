namespace Restall.HotPi.Reflow.Thermocouple
{
	public class ThermocoupleSampleToTemperatureMapper : IMapper<ThermocoupleSample, Temperature>
	{
		private readonly IMapper<ThermocoupleSample, Voltage> compensatedTemperatureToVoltageMapper;
		private readonly IMapper<Voltage, Temperature> voltageToTemperatureMapper;
		private readonly IMapper<Temperature, Temperature> coldJunctionErrorCompensation;

		public ThermocoupleSampleToTemperatureMapper(
			IMapper<ThermocoupleSample, Voltage> compensatedTemperatureToVoltageMapper,
			IMapper<Voltage, Temperature> voltageToTemperatureMapper,
			IMapper<Temperature, Temperature> coldJunctionErrorCompensation)
		{
			this.compensatedTemperatureToVoltageMapper = compensatedTemperatureToVoltageMapper;
			this.voltageToTemperatureMapper = voltageToTemperatureMapper;
			this.coldJunctionErrorCompensation = coldJunctionErrorCompensation;
		}

		public Temperature Map(ThermocoupleSample obj)
		{
			var thermocoupleVoltage = this.compensatedTemperatureToVoltageMapper.Map(obj);
			var thermocoupleTemperature = this.voltageToTemperatureMapper.Map(thermocoupleVoltage);
			var coldJunctionTemperature = this.coldJunctionErrorCompensation.Map(obj.ColdJunctionTemperature);
			return coldJunctionTemperature + thermocoupleTemperature;
		}
	}
}
