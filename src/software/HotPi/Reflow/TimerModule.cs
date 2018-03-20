using Ninject.Modules;
using Raspberry.Timers;

namespace Restall.HotPi.Reflow
{
	public class TimerModule : NinjectModule
	{
		public override void Load()
		{
			this.Kernel?.Bind<ITimer>().ToMethod(_ => Timer.Create());
		}
	}
}
