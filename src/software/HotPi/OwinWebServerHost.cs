using System;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Nancy.Bootstrapper;
using Owin;
using Restall.HotPi.Nancy;

namespace Restall.HotPi
{
	public class OwinWebServerHost : IHostWebServer, IDisposable
	{
		private readonly IHaveNancySettings settings;
		private readonly Func<INancyBootstrapper> bootstrapperFactory;
		private readonly Func<HubConfiguration> hubConfigurationFactory;

		private IDisposable owinHost;

		public OwinWebServerHost(
			IHaveNancySettings settings,
			Func<INancyBootstrapper> bootstrapperFactory,
			Func<HubConfiguration> hubConfigurationFactory)
		{
			this.settings = settings;
			this.bootstrapperFactory = bootstrapperFactory;
			this.hubConfigurationFactory = hubConfigurationFactory;
		}

		public void Start()
		{
			if (this.owinHost != null)
				return;

			this.owinHost = WebApp.Start(
				this.settings.Host,
				app => app
					.UseCors(CorsOptions.AllowAll)
					.MapSignalR("/signalr", this.hubConfigurationFactory())
					.UseNancy(cfg => cfg.Bootstrapper = this.bootstrapperFactory()));
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
