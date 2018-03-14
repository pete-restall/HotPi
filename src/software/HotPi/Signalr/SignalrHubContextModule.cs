using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using Ninject.Extensions.ContextPreservation;
using Ninject.Infrastructure.Language;
using Ninject.Modules;

namespace Restall.HotPi.Signalr
{
	public class SignalrHubContextModule : NinjectModule
	{
		public override void Load()
		{
			AllHubTypesInAssembly.ForEach(this.BindHubContext);
		}

		private static IEnumerable<Type> AllHubTypesInAssembly => AllTypes.InAssembly.Where(type => type.Inherits(GenericHubType));

		private static bool GenericHubType(Type type)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Hub<>);
		}

		private void BindHubContext(Type hubType)
		{
			var hubBaseType = hubType.GetAllBaseTypes().Single(GenericHubType);
			var clientType = hubBaseType.GenericTypeArguments[0];
			this.Kernel?
				.Bind(typeof(IHubContext<>).MakeGenericType(clientType))
				.ToMethod(ctx => GetHubContext(
					(IConnectionManager) ctx.ContextPreservingGet<IDependencyResolver>().GetService(typeof(IConnectionManager)),
					hubType,
					clientType));
		}

		private static object GetHubContext(IConnectionManager connectionManager, Type hubType, Type clientType)
		{
			return GetHubContextMethod
				.MakeGenericMethod(hubType, clientType)
				.Invoke(connectionManager, new object[0]);
		}

		private static MethodInfo GetHubContextMethod =>
			typeof(IConnectionManager)
				.GetMethods()
				.Single(x => x.Name == "GetHubContext" && x.IsGenericMethod && x.GetGenericArguments().Length == 2 && x.GetParameters().Length == 0);
	}
}
