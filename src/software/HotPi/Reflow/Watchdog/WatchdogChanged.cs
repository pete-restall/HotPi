using System;
using System.Diagnostics.CodeAnalysis;

namespace Restall.HotPi.Reflow.Watchdog
{
	[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local", Justification = CodeAnalysisJustification.ForSerialisation)]
	public class WatchdogChanged
	{
		public WatchdogChanged(DateTime timestamp, bool timerExpired)
		{
			this.Timestamp = timestamp;
			this.TimerExpired = timerExpired;
		}

		public DateTime Timestamp { get; private set; }

		public bool TimerExpired { get; private set; }
	}
}
