using System;
using CommandLine;

namespace Restall.HotPi.Reflow
{
	[Verb("reflow", HelpText = "Main Reflow Application")]
	public class ReflowControllerApplicationCommandLineArguments
	{
		public ReflowControllerApplicationCommandLineArguments(
			string uiHost,
			decimal? max31850ColdJunctionStaticErrorInCelsius,
			double? pidKp,
			double? pidKi,
			double? pidKd,
			TimeSpan? samplingInterval)
		{
			this.UiHost = uiHost;
			this.Max31850ColdJunctionStaticErrorInCelsius = max31850ColdJunctionStaticErrorInCelsius;
			this.PidKp = pidKp;
			this.PidKi = pidKi;
			this.PidKd = pidKd;
			this.SamplingInterval = samplingInterval;
		}

		[Option(longName: "ui-host", shortName: 'u', Default = "", HelpText = "The URL that will host the UI")]
		public string UiHost { get; }

		[Option(longName: "max31850-cjc-error-celsius", Default = null, HelpText = "The static error, in Celsius, of the MAX31850 Cold Junction Temperature sensor")]
		public decimal? Max31850ColdJunctionStaticErrorInCelsius { get; }

		[Option(longName: "pid-kp", Default = null, HelpText = "The Proportional coefficient (Kp) of the PID Controller")]
		public double? PidKp { get; }

		[Option(longName: "pid-ki", Default = null, HelpText = "The Integral coefficient (Ki) of the PID Controller")]
		public double? PidKi { get; }

		[Option(longName: "pid-kd", Default = null, HelpText = "The Derivative coefficient (Kd) of the PID Controller")]
		public double? PidKd { get; }

		[Option(longName: "sampling-interval", Default = null, HelpText = "The interval of time between taking temperature samples")]
		public TimeSpan? SamplingInterval { get; }
	}
}
