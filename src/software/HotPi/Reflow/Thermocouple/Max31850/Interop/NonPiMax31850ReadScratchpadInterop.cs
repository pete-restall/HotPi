using System;
using System.Diagnostics;
using System.Threading;

namespace Restall.HotPi.Reflow.Thermocouple.Max31850.Interop
{
	public class NonPiMax31850ReadScratchpadInterop : IMax31850ReadScratchpadInterop
	{
		public Max31850ReadScratchpadResponse ReadScratchpad()
		{
			Debug.WriteLine("[{0}] [{1}] Max31850ReadScratchpadInterop.ReadScratchpad()", DateTime.Now, Thread.CurrentThread.ManagedThreadId);
			return new Max31850ReadScratchpadResponse(presencePulse: false, raw: new byte[0]);
		}
	}
}
