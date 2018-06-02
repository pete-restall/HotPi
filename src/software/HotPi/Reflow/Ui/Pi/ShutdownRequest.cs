using Restall.Nancy.ServiceRouting;

namespace Restall.HotPi.Reflow.Ui.Pi
{
	[NamedRoute("pi:shutdown", "/pi", "SHUTDOWN")]
	public class ShutdownRequest
	{
		public ShutdownRequest(bool areYouSure)
		{
			this.AreYouSure = areYouSure;
		}

		public bool AreYouSure { get; }
	}
}
