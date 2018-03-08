using System;
using Nancy.Bootstrapper;
using Nancy.Hosting.Self;
using Ninject.Activation;

namespace Restall.HotPi.Nancy
{
	public class NancyHostProvider : IProvider<NancyHost>
	{
		private readonly IHaveNancySettings settings;
		private readonly Func<INancyBootstrapper> bootstrapperFactory;

		public NancyHostProvider(IHaveNancySettings settings, Func<INancyBootstrapper> bootstrapperFactory)
		{
			this.settings = settings;
			this.bootstrapperFactory = bootstrapperFactory;
		}

		public object Create(IContext context)
		{
			return new NancyHost(
				this.settings.Host,
				this.bootstrapperFactory(),
				new HostConfiguration { UrlReservations = new UrlReservations { CreateAutomatically = true } });
		}

		public Type Type => typeof(NancyHost);
	}
}
