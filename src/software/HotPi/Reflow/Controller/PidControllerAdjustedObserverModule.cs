using Ninject.Modules;

namespace Restall.HotPi.Reflow.Controller
{
	public class PidControllerAdjustedObserverModule : NinjectModule
	{
		public override void Load()
		{
			this.Kernel?
				.Bind<IObserve<PidControllerAdjusted>>()
				.To<MulticastObserverWithAggregateExceptions<PidControllerAdjusted>>()
				.WhenNoAncestorMatches(ctx => ctx.Binding.Service == typeof(IObserve<PidControllerAdjusted>));
		}
	}
}
