using System.IO;
using System.Reflection;
using Nancy;
using Nancy.Conventions;
using Nancy.Responses;

namespace Restall.HotPi.Nancy
{
	public class NancyStaticResourceConventions : IHaveNancyConventions
	{
		private const string RootAssetPath = "/assets/";

		private readonly string runningApplicationNamespace;

		public NancyStaticResourceConventions([RunningApplication] object runningApplication)
		{
			this.runningApplicationNamespace = runningApplication.GetType().Namespace;
		}

		public void ApplyConventionsTo(NancyConventions conventions)
		{
			conventions.StaticContentsConventions.Clear();
			conventions.StaticContentsConventions.Add(this.CreateAssetResponseFrom);
		}

		private Response CreateAssetResponseFrom(NancyContext context, string rootPath)
		{
			var path = context.Request.Path;
			if (!(path.StartsWith(RootAssetPath) && path.Length > RootAssetPath.Length + 1))
				return null;

			return this.AssetFromResource(path);
		}

		private Response AssetFromResource(string path)
		{
			string resourceName;
			var resourcePath = this.runningApplicationNamespace + ".Ui.Assets";
			var pathRelativeToRoot = path.Substring(RootAssetPath.Length);
			if (pathRelativeToRoot.IndexOf('/') >= 0)
			{
				resourceName = Path.GetFileName(pathRelativeToRoot);
				resourcePath += "." + pathRelativeToRoot.Substring(0, pathRelativeToRoot.Length - resourceName.Length - 1).Replace('/', '.');
			}
			else
				resourceName = pathRelativeToRoot;

			return new EmbeddedFileResponse(Assembly.GetExecutingAssembly(), resourcePath, resourceName);
		}
	}
}
