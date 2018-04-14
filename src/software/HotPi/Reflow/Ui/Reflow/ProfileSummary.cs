using System;
using System.Diagnostics.CodeAnalysis;

namespace Restall.HotPi.Reflow.Ui.Reflow
{
	[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local", Justification = CodeAnalysisJustification.ForSerialisation)]
	[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global", Justification = CodeAnalysisJustification.ForSerialisation)]
	[SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = CodeAnalysisJustification.ForSerialisation)]
	public class ProfileSummary
	{
		public ProfileSummary(Guid id, string name, Uri startReflowLink)
		{
			this.Id = id;
			this.Name = name;
			this.StartReflowLink = startReflowLink;
		}

		public Guid Id { get; private set; }

		public string Name { get; private set; }

		public Uri StartReflowLink { get; private set; }
	}
}
