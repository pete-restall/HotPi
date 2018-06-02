using Ninject.Modules;

namespace Restall.HotPi.Reflow.Profiles
{
	public class ReflowProfileRepositoryModule : NinjectModule
	{
		public override void Load()
		{
			this.Kernel?
				.Rebind<IReflowProfileRepository>()
				.To<InMemoryReflowProfileRepository>()
				.InSingletonScope();
		}
	}
}
