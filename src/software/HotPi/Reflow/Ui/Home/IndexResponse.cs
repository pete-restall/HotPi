using System.Diagnostics.CodeAnalysis;

namespace Restall.HotPi.Reflow.Ui.Home
{
	[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local", Justification = CodeAnalysisJustification.ForSerialisation)]
	[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global", Justification = CodeAnalysisJustification.ForSerialisation)]
	[SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = CodeAnalysisJustification.ForSerialisation)]
	public class IndexResponse
	{
		public IndexResponse(PiResources pi, ProfileResources profile)
		{
			this.Pi = pi;
			this.Profile = profile;
		}

		public PiResources Pi { get; private set; }

		public ProfileResources Profile { get; private set; }
	}
}
