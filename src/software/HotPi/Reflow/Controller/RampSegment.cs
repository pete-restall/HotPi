namespace Restall.HotPi.Reflow.Controller
{
	public class RampSegment : IReflowProfileSegment
	{
		private readonly Temperature target;
		private readonly Temperature maximumChangePerSecond;
		private readonly Tolerance tolerance;
		private readonly ToleranceCheck<Temperature> targetCheck;

		private bool hasBeenInitialised;
//		private decimal delta;
		private bool hasTargetBeenReached;

		public RampSegment(Temperature target, Temperature maximumChangePerSecond, Tolerance tolerance)
		{
			this.target = target;
			this.maximumChangePerSecond = maximumChangePerSecond;
			this.tolerance = tolerance;

			this.targetCheck = tolerance.ForTarget(target, TemperatureToDouble);
			this.hasBeenInitialised = false;
			this.hasTargetBeenReached = false;
//			this.delta = 0;
		}

		private static double TemperatureToDouble(Temperature x)
		{
			return decimal.ToDouble(x.Kelvin);
		}

		public Temperature GetNextSetpoint(ReflowProcessContext context)
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
/*
			Temperature setpoint;
			if (this.tolerance.ForTarget(context.Setpoint, TemperatureToDouble).IsWithinTolerance(context.ProcessVariable))
			{
				var delta = this.maximumChangePerSecond * context.IdealSampleInterval.TotalSeconds;
				if (context.ProcessVariable >= context.Setpoint)
				{
					setpoint = context.ProcessVariable - delta;
					if (setpoint < this.target)
						setpoint = this.target;
				}
				else
				{
					setpoint = context.ProcessVariable + delta;
					if (setpoint > this.target)
						setpoint = this.target;
				}
			}
			else
				setpoint = context.Setpoint;

			return setpoint;
*/
			return Temperature.Undefined;
		}

		private void Initialise(ReflowProcessContext context)
		{
/*
			this.delta = this.target >= context.ProcessVariable
				? this.maximumChangePerSecond.Kelvin
				: -this.maximumChangePerSecond.Kelvin;

			this.hasBeenInitialised = true;
*/
		}

		public bool CanProvideNextSetpoint(ReflowProcessContext context)
		{
			return !this.hasTargetBeenReached;
		}
	}
}
