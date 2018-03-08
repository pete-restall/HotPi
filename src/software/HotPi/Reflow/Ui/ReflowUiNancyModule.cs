using System;
using System.Linq;
using Nancy;
using Restall.Nancy.ServiceRouting;

namespace Restall.HotPi.Reflow.Ui
{
	public class ReflowUiNancyModule : NancyModule
	{
		public ReflowUiNancyModule(RouteRegistrar routes)
		{
			var uiNamespace = this.GetType().Namespace;
			if (uiNamespace == null)
				throw new InvalidOperationException("For some unfathomable reason there is no namespace associated with " + this.GetType());

			routes.RegisterServicesInto(
				this,
				AllTypes.InAssembly.Where(type => type.Namespace != null && type.Namespace.StartsWith(uiNamespace) && type.Name.EndsWith("Service")));
		}
	}
}
