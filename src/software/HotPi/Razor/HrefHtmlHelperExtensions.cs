using System;
using System.IO;
using HtmlTags;
using Nancy.ViewEngines.Razor;
using Newtonsoft.Json;
using Restall.HotPi.Nancy;

namespace Restall.HotPi.Razor
{
	public static class HrefHtmlHelperExtensions
	{
		public static HtmlTag Href<T>(this HtmlHelpers<T> html, Uri uri, string text, string alt = "", params string[] classes)
		{
			return new LinkTag(text, uri.ToString(), classes).Attr("alt", alt);
		}

		public static IHtmlString AsJson<T>(this HtmlHelpers<T> html, T obj)
		{
			var serialiser = html.RenderContext.Context.GetItem<JsonSerializer>();
			using (var stringWriter = new StringWriter())
			{
				serialiser.Serialize(stringWriter, obj);
				stringWriter.Flush();
				return new NonEncodedHtmlString(stringWriter.ToString());
			}
		}
	}
}
