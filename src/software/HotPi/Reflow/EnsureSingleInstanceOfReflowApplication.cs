using System;
using System.Threading;

namespace Restall.HotPi.Reflow
{
	public class EnsureSingleInstanceOfReflowApplication : IDisposable
	{
		private readonly Mutex mutex;

		public EnsureSingleInstanceOfReflowApplication()
		{
			bool createdNew;
			this.mutex = new Mutex(name: "HotPi", initiallyOwned: true, createdNew: out createdNew);
			if (!createdNew)
				throw new InvalidOperationException("There is already an instance of HotPi running.  This application uses hardware and GPIO so it should not be run multiple times.");
		}

		public void Dispose()
		{
			this.mutex.ReleaseMutex();
			this.mutex.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}
