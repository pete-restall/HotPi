using System;

namespace Restall.HotPi.Reflow.Profiles
{
	public class StartedProcessRun
	{
		public StartedProcessRun(Guid id, Guid profileId, bool wasAlreadyRunning)
		{
			this.Id = id;
			this.ProfileId = profileId;
			this.WasAlreadyRunning = wasAlreadyRunning;
		}

		public Guid Id { get; }

		public Guid ProfileId { get; }

		public bool WasAlreadyRunning { get; }
	}
}
