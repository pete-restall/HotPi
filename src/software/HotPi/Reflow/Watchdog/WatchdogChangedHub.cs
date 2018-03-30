using Microsoft.AspNet.SignalR;

namespace Restall.HotPi.Reflow.Watchdog
{
	public class WatchdogChangedHub : Hub<IObserve<WatchdogChanged>>
	{
		public void Observe(WatchdogChanged observed)
		{
			this.Clients.All.Observe(observed);
		}
	}
}
