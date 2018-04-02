using System;
using System.Collections.Generic;
using System.Linq;

namespace Restall.HotPi.Reflow.Controller
{
	public class ReflowZone
	{
		private readonly IReflowProfileSegment[] segments;
		private int currentSegment;

		public ReflowZone(params IReflowProfileSegment[] segments)
			: this((IEnumerable<IReflowProfileSegment>) segments)
		{
		}

		public ReflowZone(IEnumerable<IReflowProfileSegment> segments)
		{
			this.segments = segments.ToArray();
			if (this.segments.Length < 1)
				throw new ArgumentException("At least one segment must be given", nameof(segments));

			this.currentSegment = 0;
		}

		public bool CanProvideNextSetpoint(ReflowProcessContext context)
		{
			return this.WithSegmentForNextSetpoint(context, _ => true, () => false);
		}

		private T WithSegmentForNextSetpoint<T>(ReflowProcessContext context, Func<int, T> handle, Func<T> noHandle)
		{
			for (int i = this.currentSegment; i < this.segments.Length; i++)
				if (this.SegmentCanProvideNextSetpoint(context, i))
					return handle(i);

			return noHandle();
		}

		private bool SegmentCanProvideNextSetpoint(ReflowProcessContext context, int segmentIndex)
		{
			return segmentIndex < this.segments.Length && this.segments[segmentIndex].CanProvideNextSetpoint(context);
		}

		public Temperature GetNextSetpoint(ReflowProcessContext context)
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
