using System;

namespace Restall.HotPi.Reflow.Profiles
{
	public class RampSegment : IReflowProfileSegment
	{
		private readonly Temperature target;
		private readonly decimal maximumChangePerSecond;
		private readonly IProvideTolerance tolerance;
		private readonly ToleranceCheck<Temperature> targetCheck;

		private bool hasBeenInitialised;
		private decimal delta;
		private bool hasTargetBeenReached;

		public RampSegment(Temperature target, Temperature maximumChangePerSecond, IProvideTolerance tolerance)
		{
			if (maximumChangePerSecond.Kelvin == 0)
				throw new ArgumentOutOfRangeException(nameof(maximumChangePerSecond), maximumChangePerSecond, "Maximum change per second cannot be zero, otherwise it's not a ramp");

			this.target = target;
			this.maximumChangePerSecond = maximumChangePerSecond.Kelvin;
			this.tolerance = tolerance;

			this.targetCheck = tolerance.ForTarget(target, TemperatureToDouble);
			this.hasBeenInitialised = false;
			this.hasTargetBeenReached = false;
			this.delta = 0;
		}

		private static double TemperatureToDouble(Temperature x)
		{
			return decimal.ToDouble(x.Kelvin);
		}

		public Temperature GetNextSetpoint(IReflowProcessContext context)
		{
			if (!this.CanProvideNextSetpoint(context))
				return Temperature.Undefined;

			if (this.targetCheck.IsWithinTolerance(context.ProcessVariable))
			{
				this.hasTargetBeenReached = true;
				return this.target;
			}

			if (!this.hasBeenInitialised)
				this.Initialise(context);

			return this.tolerance.ForTarget(context.Setpoint, TemperatureToDouble).IsWithinTolerance(context.ProcessVariable)
				? this.RampToNextSetpoint(context)
				: context.Setpoint;
		}

		public bool CanProvideNextSetpoint(IReflowProcessContext context)
		{
			return !this.hasTargetBeenReached;
		}

		private void Initialise(IReflowProcessContext context)
		{
			this.delta =
				new decimal(context.SamplingInterval.TotalSeconds) *
				(this.target >= context.ProcessVariable
					? this.maximumChangePerSecond
					: -this.maximumChangePerSecond);

			this.hasBeenInitialised = true;
		}

		private Temperature RampToNextSetpoint(IReflowProcessContext context)
		{
			return this.ProcessVariableHasOvershotTarget(context)
				? this.target
				: new Temperature(context.ProcessVariable.Kelvin + this.delta);
		}

		private bool ProcessVariableHasOvershotTarget(IReflowProcessContext context)
		{
			return
				(this.delta > 0 && context.ProcessVariable > context.Setpoint) ||
				(this.delta < 0 && context.ProcessVariable < context.Setpoint);
		}
	}
}
