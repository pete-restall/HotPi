using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace Restall.HotPi.Reflow
{
	public class ReflowControllerApplication : IStartable, IStoppable, IDisposable
	{
		[SuppressMessage("ReSharper", "NotAccessedField.Local", Justification = "Lifetime control is required - prevents premature garbage collection")]
		private readonly EnsureSingleInstanceOfReflowApplication mutex;
		private readonly Func<IHostWebServer> webHostFactory;
		private readonly Func<IHostReflowPlant> plantHostFactory;
		private readonly ManualResetEventSlim stop;

		public ReflowControllerApplication(
			EnsureSingleInstanceOfReflowApplication mutex,
			Func<IHostWebServer> webHostFactory,
			Func<IHostReflowPlant> plantHostFactory)
		{
			this.mutex = mutex;
			this.webHostFactory = webHostFactory;
			this.plantHostFactory = plantHostFactory;
			this.stop = new ManualResetEventSlim();
		}

		public void Start()
		{
			var webHost = this.webHostFactory();
			if (webHost == null)
				throw new InvalidOperationException("Web Server Hosting factory returned null");

			var plantHost = this.plantHostFactory();
			if (plantHost == null)
				throw new InvalidOperationException("Plant Hosting factory returned null");

			plantHost.Start();
			using (new OnDispose(() => plantHost.Stop()))
			{
				webHost.Start();
				using (new OnDispose(() => webHost.Stop()))
				{
					this.stop.Wait();
				}
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
