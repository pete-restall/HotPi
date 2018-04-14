using System;
using System.Diagnostics.CodeAnalysis;

namespace Restall.HotPi.Reflow.Profiles
{
	[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local", Justification = CodeAnalysisJustification.ForSerialisation)]
	public class ProcessRun
	{
		public ProcessRun(Guid id, DateTime timestamp)
		{
			this.Id = id;
			this.Timestamp = timestamp;
		}

		public Guid Id { get; private set; }

		public DateTime Timestamp { get; private set; }

		public void Abort(IControlReflowProcess control)
		{
			control.Abort();
		}
	}
}
