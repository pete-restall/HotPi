using System;
using NullGuard;

namespace Restall.HotPi.Nancy
{
	public static class JsonContentTypeStringExtensions
	{
		public static bool IsJsonContentType([AllowNull] this string contentType)
		{
			if (string.IsNullOrEmpty(contentType))
				return false;

			var typeString = contentType.Split(';')[0];
			if (!typeString.Equals("application/json", StringComparison.OrdinalIgnoreCase) &&
				!typeString.StartsWith("application/json-", StringComparison.OrdinalIgnoreCase) &&
				!typeString.Equals("text/json", StringComparison.OrdinalIgnoreCase))
			{
				return typeString.EndsWith("+json", StringComparison.OrdinalIgnoreCase);
			}

			return true;
		}
	}
}
