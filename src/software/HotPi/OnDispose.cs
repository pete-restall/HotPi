using System;

namespace Restall.HotPi
{
	public class OnDispose : IDisposable
	{
		private readonly Action action;

		public OnDispose(Action action)
		{
			this.action = action;
		}

		public void Dispose()
		{
			this.action();
			GC.SuppressFinalize(this);
		}
	}
}
