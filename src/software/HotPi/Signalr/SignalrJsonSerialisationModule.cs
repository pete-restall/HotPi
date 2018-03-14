using System.Linq;
using Newtonsoft.Json;
using Ninject.Modules;

namespace Restall.HotPi.Signalr
{
	public class SignalrJsonSerialisationModule : NinjectModule
	{
		public override void Load()
		{
			this.Kernel?
				.Bind<JsonSerializer>()
				.ToMethod(ctx => JsonSerializer.CreateDefault())
				.When(ctx => ctx.Parameters.OfType<UseJsonSerialisationFor>().Any(x => x.Name == "SignalR"));
		}
	}
}
