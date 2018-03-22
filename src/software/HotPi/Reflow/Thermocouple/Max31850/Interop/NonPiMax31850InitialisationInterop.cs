using System;
using System.Diagnostics;
using System.Threading;

namespace Restall.HotPi.Reflow.Thermocouple.Max31850.Interop
{
	public class NonPiMax31850InitialisationInterop : IMax31850InitialisationInterop
	{
		public void Initialise()
		{
			Debug.WriteLine("[{0}] [{1}] Max31850InitialisationInterop.Initialise()", DateTime.Now, Thread.CurrentThread.ManagedThreadId);
		}

		public void Shutdown()
		{
			Debug.WriteLine("[{0}] [{1}] Max31850InitialisationInterop.Shutdown()", DateTime.Now, Thread.CurrentThread.ManagedThreadId);
		}
	}
}
