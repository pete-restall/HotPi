using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Ninject;
using Ninject.Syntax;
using NullGuard;

namespace Restall.HotPi.Signalr
{
	public class SignalrNinjectDependencyResolver : DefaultDependencyResolver
	{
		private readonly IResolutionRoot kernel;

		public SignalrNinjectDependencyResolver(IResolutionRoot kernel)
		{
			this.kernel = kernel;
		}

		[return: AllowNull]
		public override object GetService(Type serviceType)
		{
			return this.kernel.TryGet(serviceType, new UseJsonSerialisationFor("SignalR")) ?? base.GetService(serviceType);
		}

		public override IEnumerable<object> GetServices(Type serviceType)
		{
			return this.kernel.GetAll(serviceType).Concat(base.GetServices(serviceType));
		}
	}
}
