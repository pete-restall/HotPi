using System;
using System.Diagnostics.CodeAnalysis;

namespace Restall.HotPi.Reflow.Ui.Home
{
	[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local", Justification = CodeAnalysisJustification.ForSerialisation)]
	[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global", Justification = CodeAnalysisJustification.ForSerialisation)]
	[SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = CodeAnalysisJustification.ForSerialisation)]
	public class PiResources
	{
		public PiResources(Uri shutdownLink)
		{
			this.ShutdownLink = shutdownLink;
		}

		public Uri ShutdownLink { get; private set; }
	}
}
