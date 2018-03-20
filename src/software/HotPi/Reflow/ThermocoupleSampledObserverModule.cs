using Ninject.Modules;
using Restall.HotPi.Reflow.Thermocouple;

namespace Restall.HotPi.Reflow
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
