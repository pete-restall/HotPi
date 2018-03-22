using System;
using System.Diagnostics.CodeAnalysis;

namespace Restall.HotPi.Reflow.Watchdog
{
	public class WatchdogTimerExpired
	{
		public WatchdogTimerExpired(DateTime timestamp)
		{
			this.Timestamp = timestamp;
		}

		[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local", Justification = CodeAnalysisJustification.ForSerialisation)]
		public DateTime Timestamp { get; private set; }
	}
}
