using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Nancy.Hosting.Self;

namespace Restall.HotPi.Reflow
{
	public class ReflowControllerApplication : IStartable, IStoppable, IDisposable
	{
		[SuppressMessage("ReSharper", "NotAccessedField.Local", Justification = "Lifetime control is required - prevents premature garbage collection")]
		private readonly EnsureSingleInstanceOfReflowApplication mutex;
		private readonly Func<NancyHost> nancyHostFactory;
		private readonly ManualResetEventSlim stop;

		public ReflowControllerApplication(EnsureSingleInstanceOfReflowApplication mutex, Func<NancyHost> nancyHostFactory)
		{
			this.mutex = mutex;
			this.nancyHostFactory = nancyHostFactory;
			this.stop = new ManualResetEventSlim();
		}

		public void Start()
		{
			var nancy = this.nancyHostFactory();
			if (nancy == null)
				throw new InvalidOperationException("NancyHost factory returned null");

			try
			{
				nancy.Start();
				this.stop.Wait();
			}
			finally
			{
				nancy.Stop();
			}
		}

		public void Stop()
		{
			this.stop.Set();
		}

		public void Dispose()
		{
			this.stop.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}
