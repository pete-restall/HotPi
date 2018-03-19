using Microsoft.AspNet.SignalR;
using Ninject.Extensions.ContextPreservation;
using Ninject.Modules;

namespace Restall.HotPi.Signalr
{
	public class SignalrHubConfigurationModule : NinjectModule
	{
		public override void Load()
		{
			this.Kernel?
				.Rebind<IDependencyResolver>()
				.To<SignalrNinjectDependencyResolver>()
				.InSingletonScope();

			this.Kernel?
				.Bind<HubConfiguration>()
				.ToMethod(ctx => new HubConfiguration
				{
					EnableDetailedErrors = true,
					EnableJSONP = true,
					EnableJavaScriptProxies = true,
					Resolver = ctx.ContextPreservingGet<IDependencyResolver>()
				});
		}
	}
}
