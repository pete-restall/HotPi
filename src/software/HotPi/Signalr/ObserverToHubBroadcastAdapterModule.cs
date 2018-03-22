using System;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Ninject.Modules;
using IRequest = Ninject.Activation.IRequest;

namespace Restall.HotPi.Signalr
{
	public class ObserverToHubBroadcastAdapterModule : NinjectModule
	{
		public override void Load()
		{
			this.Kernel?
				.Bind(typeof(IObserve<>))
				.To(typeof(ObserverToHubBroadcastAdapter<>))
				.When(HubExistsForObservable);
		}

		private static bool HubExistsForObservable(IRequest context)
		{
			var observableEvent = context.Service.GenericTypeArguments[0];
			return context.ParentContext?.Kernel.GetBindings(HubTypeFor(observableEvent)).Any() == true;
		}

		private static Type HubTypeFor(Type observableEvent)
		{
			return typeof(Hub<>).MakeGenericType(typeof(IObserve<>).MakeGenericType(observableEvent));
		}
	}
}
