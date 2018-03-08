using System.Reflection;
using System.Text.RegularExpressions;
using Nancy.Conventions;
using Nancy.ViewEngines;

namespace Restall.HotPi.Nancy
{
	public class NancyViewConventions : IHaveNancyConventions
	{
		private static readonly Regex ModelNameToViewNameRegex = new Regex($"^{NancyBootstrapper.RootNamespace.Replace(".", "\\.")}\\.(?<QualifiedName>.*[^\\.])Response$", RegexOptions.Compiled);

		public void ApplyConventionsTo(NancyConventions conventions)
		{
			conventions.ViewLocationConventions.Clear(); 
			conventions.ViewLocationConventions.Add(CreatePathToViewFrom);
		}

		private static string CreatePathToViewFrom(string viewName, dynamic model, ViewLocationContext context)
		{
			var modelType = (TypeInfo) model.GetType();
			var matches = ModelNameToViewNameRegex.Match(modelType.FullName ?? "");
			if (!matches.Success)
				throw new ViewNotFoundException($"Response DTO {modelType} does not follow the naming convention.  It needs to match /{ModelNameToViewNameRegex}/");

			return matches.Groups["QualifiedName"].Value.Replace('.', '/') + "View";
		}
	}
}
