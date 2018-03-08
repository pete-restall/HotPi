using Restall.HotPi.Nancy;

namespace Restall.HotPi.Reflow.Ui.Home
{
	public class HomeService
	{
		private readonly ResourceLinker resources;

		public HomeService(ResourceLinker resources)
		{
			this.resources = resources;
		}

		public IndexResponse Index(IndexRequest request)
		{
			return new IndexResponse(this.resources.RelativeUriFor("pi:shutdown"));
		}
	}
}
