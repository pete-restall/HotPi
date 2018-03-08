using Ninject.Modules;

namespace Restall.HotPi.Reflow.Ui.Pi
{
	public class ShutdownServiceNinjectModule : NinjectModule
	{
		public override void Load()
		{
			this.Kernel?
				.Bind<IStoppable>()
				.To<SendApplicationStopAfterResponse>()
				.WhenInjectedInto<ShutdownService>();
		}
	}
}
