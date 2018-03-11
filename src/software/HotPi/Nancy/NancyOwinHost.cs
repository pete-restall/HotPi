using System;
using Microsoft.Owin.Hosting;
using Nancy.Bootstrapper;
using Owin;

namespace Restall.HotPi.Nancy
{
	public class NancyOwinHost : IHostNancy, IDisposable
	{
		private readonly IHaveNancySettings settings;
		private readonly Func<INancyBootstrapper> bootstrapperFactory;
		private IDisposable owinHost;

		public NancyOwinHost(IHaveNancySettings settings, Func<INancyBootstrapper> bootstrapperFactory)
		{
			this.settings = settings;
			this.bootstrapperFactory = bootstrapperFactory;
		}

		public void Start()
		{
			if (this.owinHost != null)
				return;

			this.owinHost = WebApp.Start(
				this.settings.Host,
				app => app.UseNancy(cfg => cfg.Bootstrapper = this.bootstrapperFactory()));
		}

		public void Dispose()
		{
			this.Stop();
			GC.SuppressFinalize(this);
		}

		public void Stop()
		{
			this.owinHost?.Dispose();
			this.owinHost = null;
		}
	}
}
