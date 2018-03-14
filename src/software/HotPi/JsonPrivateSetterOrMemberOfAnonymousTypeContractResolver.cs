using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Ninject.Infrastructure.Language;
using NullGuard;

namespace Restall.HotPi
{
	public class JsonPrivateSetterOrMemberOfAnonymousTypeContractResolver : DefaultContractResolver
	{
		public JsonPrivateSetterOrMemberOfAnonymousTypeContractResolver(NamingStrategy namingStrategy)
		{
			this.NamingStrategy = namingStrategy;
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
}
