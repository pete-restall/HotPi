using System;
using Restall.HotPi.Reflow.Thermocouple;

namespace Restall.HotPi.Reflow.Controller
{
	public class PidController : IObserve<ThermocoupleSampled>
	{
		private readonly IProvideReflowTemperatureSetpoints profile;
		private readonly IObserve<PidControllerAdjusted> observer;
		private readonly Func<DateTime> now;
		private readonly double kp;
		private readonly double ki;
		private readonly double kd;

		private DateTime lastSampleTimestamp;
		private double lastError;
		private double controlVariable;
		private double integral;
		private double derivative;

		public PidController(
			IHavePidControllerSettings settings,
			IProvideReflowTemperatureSetpoints profile,
			IObserve<PidControllerAdjusted> observer,
			Func<DateTime> now)
		{
			this.kp = settings.Kp;
			this.ki = settings.Ki;
			this.kd = settings.Kd;
			this.profile = profile;
			this.observer = observer;
			this.now = now;
			this.ResetAtTime(now());
		}

		private void ResetAtTime(DateTime timestamp)
		{
			this.lastSampleTimestamp = timestamp;
			this.lastError = 0;
			this.controlVariable = 0;
			this.integral = 0;
			this.derivative = 0;
		}

		public void Observe(ThermocoupleSampled observed)
		{
			var setpoint = this.profile.GetNextSetpoint();
			if (setpoint == Temperature.Undefined)
			{
				this.ResetAtTime(observed.Sample.Timestamp);
				return;
			}

			if (observed.Sample.ConversionFaults.AnyFault)
				return;

			this.Control(observed, setpoint);
		}

		private void Control(ThermocoupleSampled observed, Temperature setpoint)
		{
			var dt = this.lastSampleTimestamp - observed.Sample.Timestamp;
			var error = decimal.ToDouble(observed.Sample.ThermocoupleTemperature.Kelvin - setpoint.Kelvin);

			this.integral += error * dt.TotalSeconds;
			this.derivative = (error - this.lastError) / dt.TotalSeconds;
			this.controlVariable =
				this.kp * error +
				this.ki * this.integral +
				this.kd * this.derivative;

			this.lastSampleTimestamp = observed.Sample.Timestamp;
			this.lastError = error;

			this.observer.Observe(new PidControllerAdjusted(
				timestamp: this.now(),
				sampleTimestamp: observed.Sample.Timestamp,
				sampleInterval: dt,
				integral: this.integral,
				derivative: this.derivative,
				setpoint: setpoint,
				processVariable: observed.Sample.ThermocoupleTemperature,
				controlVariable: this.controlVariable));
		}
	}
}
