using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Ninject;
using Ninject.Extensions.ContextPreservation;
using Ninject.Infrastructure.Language;
using Ninject.Modules;
using NullGuard;

namespace Restall.HotPi
{
	public class JsonSerialisationModule : NinjectModule
	{
		private class PrivateSetterOrMemberOfAnonymousTypeContractResolver : DefaultContractResolver
		{
			public PrivateSetterOrMemberOfAnonymousTypeContractResolver()
			{
				this.NamingStrategy = new CamelCaseNamingStrategy();
			}

			[return: AllowNull]
			protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
			{
				var jsonProperty = base.CreateProperty(member, memberSerialization);
				if (!jsonProperty.Writable)
				{
					var property = member as PropertyInfo;
					if (property != null)
					{
						var hasPrivateSetter = property.GetSetMethod(true) != null;
						jsonProperty.Writable = hasPrivateSetter || member.DeclaringType?.HasAttribute<CompilerGeneratedAttribute>() == true;
					}
				}

				return jsonProperty.Writable || member.HasAttribute<JsonPropertyAttribute>() ? jsonProperty : null;
			}
		}

		public override void Load()
		{
			this.Kernel?.Bind<JsonSerializer>().ToMethod(ctx =>
			{
				var kernel = ctx.GetContextPreservingResolutionRoot();
				var serialiser = JsonSerializer.Create(kernel.Get<JsonSerializerSettings>());
				serialiser.ContractResolver = new PrivateSetterOrMemberOfAnonymousTypeContractResolver();
				kernel.GetAll<JsonConverter>().ForEach(serialiser.Converters.Add);
				return serialiser;
			});
		}
	}
}
