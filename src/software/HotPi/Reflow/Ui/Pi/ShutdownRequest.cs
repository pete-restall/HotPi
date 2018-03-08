using Restall.Nancy.ServiceRouting;

namespace Restall.HotPi.Reflow.Ui.Pi
{
	[NamedRoute("pi:shutdown", "/pi/shutdown", "POST")]
	public class ShutdownRequest
	{
		public ShutdownRequest(bool areYouSure)
		{
			this.AreYouSure = areYouSure;
		}

		public bool AreYouSure { get; }
	}
}
