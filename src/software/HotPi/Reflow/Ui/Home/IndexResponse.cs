using System.Diagnostics.CodeAnalysis;

namespace Restall.HotPi.Reflow.Ui.Home
{
	[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local", Justification = CodeAnalysisJustification.ForSerialisation)]
	public class IndexResponse
	{
		public IndexResponse(PiResources pi, ReflowResources reflow)
		{
			this.Pi = pi;
			this.Reflow = reflow;
		}

		public PiResources Pi { get; private set; }

		public ReflowResources Reflow { get; private set; }
	}
}
