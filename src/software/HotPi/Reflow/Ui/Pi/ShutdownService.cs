namespace Restall.HotPi.Reflow.Ui.Pi
{
	public class ShutdownService
	{
		private readonly IStoppable application;

		public ShutdownService(IStoppable application)
		{
			this.application = application;
		}

		public object Shutdown(ShutdownRequest request)
		{
			if (!request.AreYouSure)
				return new NotShuttingDownResponse();

			this.application.Stop();
			return new ShuttingDownResponse("TODO - THIS NEEDS TO BE THE IP OF THE CURRENT MACHINE...(FOR PINGING !)");
		}
	}
}
