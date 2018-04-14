using System;
using System.Collections.Generic;
using System.Linq;

namespace Restall.HotPi.Reflow.Profiles
{
	public class StartProcessRunCommand
	{
		public StartProcessRunCommand(Guid processRunId, Guid profileId, IEnumerable<ReflowZone> zones)
		{
			this.ProcessRunId = processRunId;
			this.ProfileId = profileId;
			this.Zones = zones.ToArray();
		}

		public Guid ProcessRunId { get; }

		public Guid ProfileId { get; }

		public IEnumerable<ReflowZone> Zones { get; }
	}
}
