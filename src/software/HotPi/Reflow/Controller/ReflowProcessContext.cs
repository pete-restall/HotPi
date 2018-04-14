using System;
using Restall.HotPi.Reflow.Profiles;

namespace Restall.HotPi.Reflow.Controller
{
	public class ReflowProcessContext : IReflowProcessContext
	{
		private static readonly TimeSpan MinimumReasonableSamplingInterval = 100.Milliseconds();

		private readonly PidControllerAdjusted lastPidAdjustment;

		public ReflowProcessContext(TimeSpan samplingInterval, PidControllerAdjusted lastPidAdjustment)
		{
			if (samplingInterval < MinimumReasonableSamplingInterval)
				throw new ArgumentOutOfRangeException(nameof(samplingInterval), samplingInterval, "Sampling interval is unreasonable; it should be at least " + MinimumReasonableSamplingInterval);

			this.SamplingInterval = samplingInterval;
			this.lastPidAdjustment = lastPidAdjustment;
		}

		public TimeSpan SamplingInterval { get; }

		public Temperature ProcessVariable => this.lastPidAdjustment.ProcessVariable;

		public Temperature Setpoint => this.lastPidAdjustment.Setpoint;
	}
}
