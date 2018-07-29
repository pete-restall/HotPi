using Ninject.Extensions.ContextPreservation;
using Ninject.Modules;
using Restall.HotPi.Reflow.Controller.Actuators;
using Restall.HotPi.Reflow.Profiles;

namespace Restall.HotPi.Reflow.Controller
{
	public class ReflowProcessControllerModule : NinjectModule
	{
		public override void Load()
		{
			this.Kernel?
				.Rebind<IControlReflowProcess>()
				.To<FanSsr>()
				.WithConstructorArgument(typeof(IControlReflowProcess), ctx => ctx.ContextPreservingGet<ReflowProcessController>());

			this.Kernel?
				.Rebind<ReflowProcessController, IProvideReflowTemperatureSetpoints, IObserve<PidControllerAdjusted>>()
				.To<ReflowProcessController>()
				.InSingletonScope();
		}
	}
}
