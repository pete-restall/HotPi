using System;
using System.Diagnostics.CodeAnalysis;
using Restall.Nancy.ServiceRouting;

namespace Restall.HotPi.Reflow.Ui.Reflow
{
	[NamedRoute("reflow:start", "/reflow/{profileId}/process-run", "START")]
	public class StartProcessRunRequest
	{
		[SuppressMessage("ReSharper", "UnusedMember.Local", Justification = CodeAnalysisJustification.Pola)]
		public StartProcessRunRequest(Guid profileId)
		{
			this.ProfileId = profileId;
		}

		[SuppressMessage("ReSharper", "UnusedMember.Local", Justification = CodeAnalysisJustification.ForSerialisation)]
		private StartProcessRunRequest() { }

		[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local", Justification = CodeAnalysisJustification.ForSerialisation)]
		public Guid ProfileId { get; private set; }
	}
}
