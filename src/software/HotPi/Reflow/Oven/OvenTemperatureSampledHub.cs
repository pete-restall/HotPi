using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNet.SignalR;

namespace Restall.HotPi.Reflow.Oven
{
	public class OvenTemperatureSampledHub : Hub<IObserve<OvenTemperatureSampled>>
	{
		[SuppressMessage("ReSharper", "UnusedMember.Global", Justification = CodeAnalysisJustification.HubMethod)]
		public void Observe(OvenTemperatureSampled observed)
		{
			this.Clients.All.Observe(observed);
		}
	}
}
