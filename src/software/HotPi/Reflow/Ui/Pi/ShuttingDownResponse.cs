using System;

namespace Restall.HotPi.Reflow.Ui.Pi
{
	public class ShuttingDownResponse
	{
		public ShuttingDownResponse(string pingForLifesigns)
		{
			this.PingForLifesigns = pingForLifesigns;
		}

		public bool IsShuttingDown => true;

		public string PingForLifesigns { get; }
	}
}
