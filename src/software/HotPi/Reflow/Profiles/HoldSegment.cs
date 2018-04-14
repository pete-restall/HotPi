using System;
using System.Diagnostics;

namespace Restall.HotPi.Reflow.Profiles
{
	public class HoldSegment : IReflowProfileSegment
	{
		private readonly Temperature target;
		private readonly TimeSpan length;

		private Stopwatch stopwatch;

		public HoldSegment(Temperature target, TimeSpan length)
		{
			if (length < 1.Seconds())
				throw new ArgumentOutOfRangeException(nameof(length), length, "Length of hold segment should be at least one second long");

			this.target = target;
			this.length = length;
		}

		public Temperature GetNextSetpoint(IReflowProcessContext context)
		{
			if (this.stopwatch == null)
				this.stopwatch = Stopwatch.StartNew();

			return this.target;
		}

		public bool CanProvideNextSetpoint(IReflowProcessContext context)
		{
			return this.stopwatch?.ElapsedTicks < this.length.Ticks;
		}
	}
}
