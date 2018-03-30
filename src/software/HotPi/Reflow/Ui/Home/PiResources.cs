using System;
using System.Diagnostics.CodeAnalysis;

namespace Restall.HotPi.Reflow.Ui.Home
{
	public class PiResources
	{
		public PiResources(Uri shutdownLink)
		{
			this.ShutdownLink = shutdownLink;
		}

		[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local", Justification = CodeAnalysisJustification.ForSerialisation)]
		public Uri ShutdownLink { get; private set; }
	}
}
