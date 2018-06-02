using System.Diagnostics.CodeAnalysis;
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

		[SuppressMessage("ReSharper", "UnusedMember.Global", Justification = CodeAnalysisJustification.Rpc)]
		[SuppressMessage("ReSharper", "UnusedParameter.Global", Justification = CodeAnalysisJustification.ForRouting)]
		public IndexResponse Index(IndexRequest request)
		{
			return new IndexResponse(
				new PiResources(this.resources.RelativeUriFor("pi:shutdown")),
				new ProfileResources(this.resources.RelativeUriFor("reflow:summaryOfAllProfiles")));
		}
	}
}
