using System;
using System.Collections.Generic;
using System.Linq;

namespace Restall.HotPi.Reflow.Profiles
{
	public class ReflowZone
	{
		private readonly string name;
		private readonly IReflowProfileSegment[] segments;

		private int currentSegment;

		public ReflowZone(string name, params IReflowProfileSegment[] segments)
			: this(name, (IEnumerable<IReflowProfileSegment>) segments)
		{
		}

		public ReflowZone(string name, IEnumerable<IReflowProfileSegment> segments)
		{
			this.name = name.Trim();
			if (this.name == string.Empty)
				throw new ArgumentException("Name must be supplied", nameof(name));

			this.segments = segments.ToArray();
			if (this.segments.Length < 1)
				throw new ArgumentException("At least one segment must be given", nameof(segments));

			this.currentSegment = 0;
		}

		public bool CanProvideNextSetpoint(IReflowProcessContext context)
		{
			return this.WithSegmentForNextSetpoint(context, _ => true, () => false);
		}

		private T WithSegmentForNextSetpoint<T>(IReflowProcessContext context, Func<int, T> handle, Func<T> noHandle)
		{
			for (int i = this.currentSegment; i < this.segments.Length; i++)
				if (this.SegmentCanProvideNextSetpoint(context, i))
					return handle(i);

			return noHandle();
		}

		private bool SegmentCanProvideNextSetpoint(IReflowProcessContext context, int segmentIndex)
		{
			return segmentIndex < this.segments.Length && this.segments[segmentIndex].CanProvideNextSetpoint(context);
		}

		public Temperature GetNextSetpoint(IReflowProcessContext context)
		{
			return this.WithSegmentForNextSetpoint(
				context,
				segment =>
				{
					this.currentSegment = segment;
					return this.segments[segment].GetNextSetpoint(context);
				},
				() => Temperature.Undefined);
		}
	}
}
