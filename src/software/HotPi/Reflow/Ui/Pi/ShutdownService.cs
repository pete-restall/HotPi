using Restall.HotPi.Nancy;

namespace Restall.HotPi.Reflow.Ui.Pi
{
	public class ShutdownService
	{
		private readonly IHaveNancySettings nancySettings;
		private readonly IStoppable application;

		public ShutdownService(IHaveNancySettings nancySettings, IStoppable application)
		{
			this.nancySettings = nancySettings;
			this.application = application;
		}

		public object Shutdown(ShutdownRequest request)
		{
			if (!request.AreYouSure)
				return new NotShuttingDownResponse();

			this.application.Stop();
			return new ShuttingDownResponse(this.nancySettings.Host.DnsSafeHost);
		}
	}
}
