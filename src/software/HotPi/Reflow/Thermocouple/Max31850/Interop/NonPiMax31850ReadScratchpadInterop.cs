using System;
using System.Diagnostics;
using System.Threading;

namespace Restall.HotPi.Reflow.Thermocouple.Max31850.Interop
{
	public class NonPiMax31850ReadScratchpadInterop : IMax31850ReadScratchpadInterop
	{
		private readonly Func<DateTime> now;

		public NonPiMax31850ReadScratchpadInterop(Func<DateTime> now)
		{
			this.now = now;
		}

		public Max31850ReadScratchpadResponse ReadScratchpad()
		{
			Debug.WriteLine("[{0}] [{1}] Max31850ReadScratchpadInterop.ReadScratchpad()", this.now(), Thread.CurrentThread.ManagedThreadId);
			return new Max31850ReadScratchpadResponse(presencePulse: false, raw: new byte[0]);
		}
	}
}
