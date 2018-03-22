using Microsoft.AspNet.SignalR;

namespace Restall.HotPi.Signalr
{
	[DoesNotParticipateInBindingConventions]
	public class ObserverToHubBroadcastAdapter<TEvent> : IObserve<TEvent>
	{
		private readonly IHubContext<IObserve<TEvent>> hub;

		public ObserverToHubBroadcastAdapter(IHubContext<IObserve<TEvent>> hub)
		{
			this.hub = hub;
		}

		public void Observe(TEvent observed)
		{
			this.hub.Clients.All.Observe(observed);
		}
	}
}
