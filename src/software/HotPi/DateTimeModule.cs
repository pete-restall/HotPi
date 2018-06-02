using System;
using Ninject.Modules;

namespace Restall.HotPi
{
	public class DateTimeModule : NinjectModule
	{
		public override void Load()
		{
			this.Kernel?
				.Bind<Func<DateTime>>()
				.ToMethod(_ => () => DateTime.UtcNow);
		}
	}
}
