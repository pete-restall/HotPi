using System.IO;
using Nancy.ModelBinding;
using Newtonsoft.Json;
using NullGuard;

namespace Restall.HotPi.Nancy
{
	public class NancyJsonBodyDeserialiser : IBodyDeserializer
	{
		private readonly JsonSerializer serialiser;

		public NancyJsonBodyDeserialiser(JsonSerializer serialiser)
		{
			this.serialiser = serialiser;
		}

		[return: AllowNull]
		public object Deserialize(string contentType, Stream bodyStream, BindingContext context)
		{
			if (!this.CanDeserialize(contentType, context))
				return null;

			using (var streamReader = new StreamReader(bodyStream))
			{
				using (var jsonTextReader = new JsonTextReader(streamReader))
				{
					return this.serialiser.Deserialize(jsonTextReader, context.DestinationType);
				}
			}
		}

		public bool CanDeserialize([AllowNull] string contentType, [AllowNull] BindingContext context)
		{
			return contentType.IsJsonContentType();
		}
	}
}
