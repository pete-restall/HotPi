using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Nancy;
using Newtonsoft.Json;
using NullGuard;

namespace Restall.HotPi.Nancy
{
	public class NancyJsonSerialiser : ISerializer
	{
		private readonly JsonSerializer serialiser;

		public NancyJsonSerialiser(JsonSerializer serialiser)
		{
			this.serialiser = serialiser;
		}

		public bool CanSerialize([AllowNull] string contentType)
		{
			return contentType.IsJsonContentType();
		}

		public void Serialize<TModel>(string contentType, TModel model, Stream outputStream)
		{
			if (!this.CanSerialize(contentType))
				throw new InvalidOperationException($"Cannot serialise non-JSON content type '{contentType}'");

			using (var streamWriter = new StreamWriter(outputStream, Utf8WithoutBom, bufferSize: 1024, leaveOpen: true))
			{
				this.serialiser.Serialize(streamWriter, model);
			}
		}

		private static readonly Encoding Utf8WithoutBom = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true);

		public IEnumerable<string> Extensions => new[] {"json"};
	}
}
