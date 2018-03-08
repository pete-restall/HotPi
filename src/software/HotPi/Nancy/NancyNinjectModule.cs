using Nancy.Hosting.Self;
using Ninject.Modules;

namespace Restall.HotPi.Nancy
{
	public class NancyNinjectModule : NinjectModule
	{
		public override void Load()
		{
			this.Kernel?
				.Bind<NancyHost>()
				.ToProvider<NancyHostProvider>();
		}
	}
}
