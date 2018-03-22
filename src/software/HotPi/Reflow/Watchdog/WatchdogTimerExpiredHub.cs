using Microsoft.AspNet.SignalR;

namespace Restall.HotPi.Reflow.Watchdog
{
	public class WatchdogTimerExpiredHub : Hub<IObserve<WatchdogTimerExpired>>
	{
		public void Observe(WatchdogTimerExpired observed)
		{
			this.Clients.All.Observe(observed);
		}
	}
}
