using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Ninject;
using Nancy.Conventions;
using Nancy.Diagnostics;
using Nancy.ViewEngines;
using Newtonsoft.Json;
using Ninject;
using Ninject.Extensions.ChildKernel;
using Ninject.Extensions.ContextPreservation;
using Ninject.Syntax;
using Restall.Nancy.ServiceRouting;

namespace Restall.HotPi.Nancy
{
	public class NancyBootstrapper : NinjectNancyBootstrapper
	{
		public static readonly string RootNamespace = typeof(Program).Namespace;

		private readonly IKernel kernel;
		private readonly IEnumerable<IHaveNancyConventions> conventions;

		public NancyBootstrapper(IResolutionRoot kernel, IEnumerable<IHaveNancyConventions> conventions)
		{
			this.kernel = new ChildKernel(kernel);
			this.conventions = conventions.ToArray();
		}

		protected override DiagnosticsConfiguration DiagnosticsConfiguration => new DiagnosticsConfiguration { Password = "debug" };

		protected override IKernel GetApplicationContainer()
		{
			return this.kernel;
		}

		protected override NancyInternalConfiguration InternalConfiguration => NancyInternalConfiguration.WithOverrides(OnConfiguration);

		private static void OnConfiguration(NancyInternalConfiguration config)
		{
			config.ViewLocationProvider = typeof(ResourceViewLocationProvider);
			if (!ResourceViewLocationProvider.RootNamespaces.ContainsKey(Assembly.GetExecutingAssembly()))
				ResourceViewLocationProvider.RootNamespaces.Add(Assembly.GetExecutingAssembly(), RootNamespace);
		}

		protected override void ConfigureConventions(NancyConventions nancyConventions)
		{
			this.conventions.ForEach(x => x.ApplyConventionsTo(nancyConventions));
		}

		protected override void ConfigureRequestContainer(IKernel container, NancyContext context)
		{
			base.ConfigureRequestContainer(container, context);
			container.Bind<NancyContext>().ToConstant(context).InTransientScope();
			container.Bind<RouteRegistrar>().ToMethod(ctx => RouteRegistrarFactory.CreateDefaultInstance(type => ctx.ContextPreservingGet(type)));

			context.SetLazyItem(() => container.Get<JsonSerializer>());
		}
	}
}
