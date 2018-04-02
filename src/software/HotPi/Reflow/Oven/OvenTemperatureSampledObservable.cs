using Restall.HotPi.Reflow.Thermocouple;

namespace Restall.HotPi.Reflow.Oven
{
	public class OvenTemperatureSampledObservable : IObserve<ThermocoupleSampled>
	{
		private readonly IObserve<OvenTemperatureSampled> observer;
		private readonly IMapper<ThermocoupleSample, Temperature> temperatureMapper;

		public OvenTemperatureSampledObservable(
			IObserve<OvenTemperatureSampled> observer,
			IMapper<ThermocoupleSample, Temperature> temperatureMapper)
		{
			this.observer = observer;
			this.temperatureMapper = temperatureMapper;
		}

		void IObserve<ThermocoupleSampled>.Observe(ThermocoupleSampled observed)
		{
			if (observed.Sample.ConversionFaults.AnyFault)
				return;

			this.observer.Observe(new OvenTemperatureSampled(observed.Sample.Timestamp, this.temperatureMapper.Map(observed.Sample)));
		}
	}
}
