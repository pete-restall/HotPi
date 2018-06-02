using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Restall.HotPi.Reflow.Profiles
{
	[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local", Justification = CodeAnalysisJustification.ForSerialisation)]
	public class ReflowProfile
	{
		private readonly List<ProcessRun> processRuns;
		private readonly Func<DateTime> now;

		public ReflowProfile(
			Guid id,
			string name,
			IEnumerable<ProcessRun> processRuns,
			IEnumerable<ReflowZone> zones,
			Func<DateTime> now)
		{
			this.Id = id;
			this.Name = name.Trim();
			if (this.Name == string.Empty)
				throw new ArgumentException("Name must be specified", nameof(name));

			this.ProcessRuns = this.processRuns = processRuns.ToList();
			this.Zones = zones.ToArray();
			this.now = now;
		}

		public Guid Id { get; private set; }

		public string Name { get; private set; }

		public IEnumerable<ProcessRun> ProcessRuns { get; private set; }

		public IEnumerable<ReflowZone> Zones { get; }

		public StartedProcessRun TryStartProcessRun(IControlReflowProcess control)
		{
			var processRun = new ProcessRun(
				id: Guid.NewGuid(),
				timestamp: this.now());

			var startCommand = new StartProcessRunCommand(
				processRunId: processRun.Id,
				profileId: this.Id,
				zones: this.Zones);

			bool alreadyRunning = !control.TryStart(startCommand, out var running);
			if (!alreadyRunning)
				this.processRuns.Add(processRun);

			return new StartedProcessRun(
				id: running.ProcessRunId,
				profileId: running.ProfileId,
				wasAlreadyRunning: alreadyRunning);
		}

		public bool AbortProcessRun(Guid id, IControlReflowProcess control)
		{
			var run = this.processRuns.FirstOrDefault(x => x.Id == id);
			if (run == null)
				return false;

			run.Abort(control);
			return true;
		}
	}
}
