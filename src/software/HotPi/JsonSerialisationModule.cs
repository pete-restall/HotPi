using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Ninject;
using Ninject.Extensions.ContextPreservation;
using Ninject.Modules;

namespace Restall.HotPi
{
	public class JsonSerialisationModule : NinjectModule
	{
		public override void Load()
		{
			this.Kernel?.Bind<JsonSerializer>().ToMethod(ctx =>
			{
				var kernel = ctx.GetContextPreservingResolutionRoot();
				var serialiser = JsonSerializer.Create(kernel.Get<JsonSerializerSettings>());
				serialiser.ContractResolver = new JsonPrivateSetterOrMemberOfAnonymousTypeContractResolver(new CamelCaseNamingStrategy());
				kernel.GetAll<JsonConverter>().ForEach(serialiser.Converters.Add);
				return serialiser;
			});
		}
	}
}
