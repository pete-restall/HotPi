using System;
using System.Diagnostics.CodeAnalysis;

namespace Restall.HotPi.Reflow.Ui.Home
{
	public class ReflowResources
	{
		public ReflowResources(Uri startLink)
		{
			this.StartLink = startLink;
		}

		[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local", Justification = CodeAnalysisJustification.ForSerialisation)]
		public Uri StartLink { get; private set; }
	}
}
