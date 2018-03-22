using System;
using System.Collections.Generic;
using System.Linq;

namespace Restall.HotPi.Reflow
{
	public class PcbReflowPlant : IHostReflowPlant, IDisposable
	{
		private readonly IEnumerable<IReflowPlantProcess> processes;

		public PcbReflowPlant(IEnumerable<IReflowPlantProcess> processes)
		{
			this.processes = processes.ToArray();
		}

		public void Start()
		{
			this.processes.ForEach(x => x.Start());
		}

		public void Stop()
		{
			this.processes.ForEach(x => x.Stop());
		}

		public void Dispose()
		{
			this.Stop();
			GC.SuppressFinalize(this);
		}
	}
}
