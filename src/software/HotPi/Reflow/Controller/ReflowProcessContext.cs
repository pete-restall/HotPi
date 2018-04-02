using System;

namespace Restall.HotPi.Reflow.Controller
{
	public class ReflowProcessContext
	{
		private readonly PidControllerAdjusted lastPidAdjustment;

		public ReflowProcessContext(PidControllerAdjusted lastPidAdjustment)
		{
			this.lastPidAdjustment = lastPidAdjustment;
		}

		public DateTime SampleTimestamp => this.lastPidAdjustment.SampleTimestamp;

		public Temperature ProcessVariable => this.lastPidAdjustment.ProcessVariable;

		public Temperature Setpoint => this.lastPidAdjustment.Setpoint;
	}
}
