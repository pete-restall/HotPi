using HtmlTags;
using Nancy.ViewEngines.Razor;

namespace Restall.HotPi.Razor
{
	public static class HtmlTagExtensions
	{
		public static IHtmlString AsRaw(this HtmlTag tag)
		{
			return new NonEncodedHtmlString(tag.ToString());
		}
	}
}
