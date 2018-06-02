using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Restall.HotPi.Reflow.Ui.Reflow
{
	[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local", Justification = CodeAnalysisJustification.ForSerialisation)]
	[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global", Justification = CodeAnalysisJustification.ForSerialisation)]
	[SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = CodeAnalysisJustification.ForSerialisation)]
	public class GetSummaryOfAllProfilesResponse
	{
		public GetSummaryOfAllProfilesResponse(IEnumerable<ProfileSummary> summaries)
		{
			this.Profiles = summaries.ToArray();
		}

		public IEnumerable<ProfileSummary> Profiles { get; private set; }
	}
}
