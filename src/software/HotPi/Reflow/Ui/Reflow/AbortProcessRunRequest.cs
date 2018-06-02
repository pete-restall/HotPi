using System;
using System.Diagnostics.CodeAnalysis;
using Restall.Nancy.ServiceRouting;

namespace Restall.HotPi.Reflow.Ui.Reflow
{
	[NamedRoute("reflow:abort", "/reflow/{profileId}/process-run/{processRunId}", "ABORT")]
	[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local", Justification = CodeAnalysisJustification.ForSerialisation)]
	public class AbortProcessRunRequest
	{
		public AbortProcessRunRequest(Guid profileId, Guid processRunId)
		{
			this.ProfileId = profileId;
			this.ProcessRunId = processRunId;
		}

		[SuppressMessage("ReSharper", "UnusedMember.Local", Justification = CodeAnalysisJustification.ForSerialisation)]
		private AbortProcessRunRequest() { }

		public Guid ProfileId { get; private set; }

		public Guid ProcessRunId { get; private set; }
	}
}
