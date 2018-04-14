using System;
using System.Diagnostics.CodeAnalysis;

namespace Restall.HotPi.Reflow.Ui.Home
{
	[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local", Justification = CodeAnalysisJustification.ForSerialisation)]
	[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global", Justification = CodeAnalysisJustification.ForSerialisation)]
	[SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = CodeAnalysisJustification.ForSerialisation)]
	public class ProfileResources
	{
		public ProfileResources(Uri summaryOfAllProfilesLink)
		{
			this.SummaryOfAllProfilesLink = summaryOfAllProfilesLink;
		}

		public Uri SummaryOfAllProfilesLink { get; private set; }
	}
}
