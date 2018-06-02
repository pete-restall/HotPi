using System;
using System.Collections.Generic;
using System.Linq;
using NullGuard;

namespace Restall.HotPi.Reflow.Profiles
{
	public class InMemoryReflowProfileRepository : IReflowProfileRepository
	{
		private static readonly IProvideTolerance RampTolerance = 1.PercentTolerance();

		private readonly List<ReflowProfile> profiles;

		public InMemoryReflowProfileRepository(Func<DateTime> now)
		{
			this.profiles = new List<ReflowProfile>
			{
				new ReflowProfile(
					id: Guid.Parse("6a94dd00-f6fb-4feb-8000-191a504ea1ff"),
					name: "SnCu",
					processRuns: Enumerable.Empty<ProcessRun>(),
					zones: new[]
					{
						new ReflowZone(
							"Pre-Heat",
							new RampSegment(
								target: 180.Celsius(),
								maximumChangePerSecond: 5.Celsius(),
								tolerance: RampTolerance)),
						new ReflowZone(
							"Soak",
							new HoldSegment(
								target: 180.Celsius(),
								length: 90.Seconds())),
						new ReflowZone(
							"Reflow",
							new RampSegment(
								target: 217.Celsius(),
								maximumChangePerSecond: 1.Celsius(),
								tolerance: RampTolerance),
							new RampSegment(
								target: 258.Celsius(),
								maximumChangePerSecond: 3.Celsius(),
								tolerance: RampTolerance),
							new HoldSegment(
								target: 260.Celsius(),
								length: 5.Seconds())),
						new ReflowZone(
							"Cooldown",
							new RampSegment(
								target: 25.Celsius(),
								maximumChangePerSecond: 5.Celsius(),
								tolerance: RampTolerance))
					},
					now: now)
			};
		}

		public bool TryGetById(Guid id, [AllowNull] out ReflowProfile profile)
		{
			profile = this.profiles.SingleOrDefault(x => x.Id == id);
			return profile != null;
		}

		public IEnumerable<ReflowProfile> GetAll()
		{
			return this.profiles;
		}
	}
}
