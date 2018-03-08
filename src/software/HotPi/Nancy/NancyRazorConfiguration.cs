using System;
using System.Collections.Generic;
using HtmlTags;
using Nancy.ViewEngines.Razor;
using Restall.HotPi.Razor;

namespace Restall.HotPi.Nancy
{
	public class NancyRazorConfiguration : IRazorConfiguration
	{
		public IEnumerable<string> GetAssemblyNames()
		{
			yield return this.GetType().Assembly.FullName;
			yield return typeof(HtmlTag).Assembly.FullName;
			yield return typeof(Uri).Assembly.FullName;
			yield return typeof(System.Web.IHtmlString).Assembly.FullName;
		}

		public IEnumerable<string> GetDefaultNamespaces()
		{
			yield return typeof(HrefHtmlHelperExtensions).Namespace;
		}

		public bool AutoIncludeModelNamespace => true;
	}
}
