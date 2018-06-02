using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Restall.HotPi.Nancy;
using Restall.HotPi.Reflow.Profiles;

namespace Restall.HotPi.Reflow.Ui.Reflow
{
	public class ProfileSummaryService
	{
		private readonly IReflowProfileRepository profiles;
		private readonly ResourceLinker resources;

		public ProfileSummaryService(IReflowProfileRepository profiles, ResourceLinker resources)
		{
			this.profiles = profiles;
			this.resources = resources;
		}

		[SuppressMessage("ReSharper", "UnusedMember.Global", Justification = CodeAnalysisJustification.Rpc)]
		[SuppressMessage("ReSharper", "UnusedParameter.Global", Justification = CodeAnalysisJustification.ForRouting)]
		public object GetSummary(GetSummaryOfAllProfilesRequest request)
		{
			return new GetSummaryOfAllProfilesResponse(
				this.profiles.GetAll().Select(x => new ProfileSummary(
					id: x.Id,
					name: x.Name,
					startReflowLink: this.resources.RelativeUriFor("reflow:start", new { profileId = x.Id })))
			);
		}
	}
}
