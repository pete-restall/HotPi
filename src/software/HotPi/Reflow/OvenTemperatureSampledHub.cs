using Microsoft.AspNet.SignalR;

namespace Restall.HotPi.Reflow
{
	public class OvenTemperatureSampledHub : Hub<IObserve<OvenTemperatureSampled>>
	{
		public void Observe(OvenTemperatureSampled observed)
		{
			this.Clients.All.Observe(observed);
		}
	}
}
