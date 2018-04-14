using Ninject.Modules;
using Restall.HotPi.Reflow.Profiles;

namespace Restall.HotPi.Reflow.Controller
{
	public class ReflowProcessControllerModule : NinjectModule
	{
		public override void Load()
		{
			this.Kernel?
				.Rebind<IControlReflowProcess, IProvideReflowTemperatureSetpoints, IObserve<PidControllerAdjusted>>()
				.To<ReflowProcessController>()
				.InSingletonScope();
		}
	}
}
