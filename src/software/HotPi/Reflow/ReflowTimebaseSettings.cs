using System;
using System.Configuration;
namespace Restall.HotPi.Reflow
{
	public class ReflowTimebaseSettings : IHaveReflowTimebaseSettings
	{
		public ReflowTimebaseSettings(ReflowControllerApplicationCommandLineArguments args)
		{
			this.SamplingInterval = args.SamplingInterval ?? FromAppSettingsOrDefault("reflow:samplingInterval", 500.Milliseconds());
			if (this.SamplingInterval < 200.Milliseconds())
				throw new InvalidOperationException("Sampling interval " + this.SamplingInterval + " is below a reasonable minimum of 200ms");
		}

		private static TimeSpan FromAppSettingsOrDefault(string key, TimeSpan defaultValue)
		{
			if (!TimeSpan.TryParse(ConfigurationManager.AppSettings[key]?.Trim(), out var value))
				return defaultValue;

			return value;
		}

		public TimeSpan SamplingInterval { get; }
	}
}
