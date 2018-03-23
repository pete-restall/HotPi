using System.Diagnostics.CodeAnalysis;

namespace Restall.HotPi.Reflow.Ui.Reflow
{
	public class ReflowService
	{
		[SuppressMessage("ReSharper", "UnusedParameter.Global", Justification = CodeAnalysisJustification.ForRouting)]
		public object Start(StartRequest request)
		{
			return new StartResponse();
		}
	}
}
