using System;
using System.Diagnostics.CodeAnalysis;

namespace Restall.HotPi.Reflow.Ui.Reflow
{
	[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local", Justification = CodeAnalysisJustification.ForSerialisation)]
	[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global", Justification = CodeAnalysisJustification.ForSerialisation)]
	[SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = CodeAnalysisJustification.ForSerialisation)]
	public class StartProcessRunResponse
	{
		public StartProcessRunResponse(
			Guid profileId,
			Guid processRunId,
			Uri abortLink)
		{
			this.ProfileId = profileId;
			this.ProcessRunId = processRunId;
			this.AbortLink = abortLink;
		}

		public Guid ProfileId { get; private set; }

		public Guid ProcessRunId { get; private set; }

		public Uri AbortLink { get; private set; }
	}
}
