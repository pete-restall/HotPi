using Ninject.Modules;

namespace Restall.HotPi.Reflow.Thermocouple
{
	public class ThermocoupleSampledObserverModule : NinjectModule
	{
		public override void Load()
		{
			this.Kernel?
				.Bind<IObserve<ThermocoupleSampled>>()
				.To<MulticastObserverWithAggregateExceptions<ThermocoupleSampled>>()
				.WhenInjectedExactlyInto<ThermocoupleMonitoringProcess>();
		}
	}
}
