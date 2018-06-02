using System;
using System.Diagnostics;
using System.Threading;

namespace Restall.HotPi.Reflow.Thermocouple.Max31850.Interop
{
	public class NonPiMax31850InitialisationInterop : IMax31850InitialisationInterop
	{
		private readonly Func<DateTime> now;

		public NonPiMax31850InitialisationInterop(Func<DateTime> now)
		{
			this.now = now;
		}

		public void Initialise()
		{
			Debug.WriteLine("[{0}] [{1}] Max31850InitialisationInterop.Initialise()", this.now(), Thread.CurrentThread.ManagedThreadId);
		}

		public void Shutdown()
		{
			Debug.WriteLine("[{0}] [{1}] Max31850InitialisationInterop.Shutdown()", this.now(), Thread.CurrentThread.ManagedThreadId);
		}
	}
}
