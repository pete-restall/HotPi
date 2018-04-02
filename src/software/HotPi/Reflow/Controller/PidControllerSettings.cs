using System.Configuration;

namespace Restall.HotPi.Reflow.Controller
{
	public class PidControllerSettings : IHavePidControllerSettings
	{
		public PidControllerSettings(ReflowControllerApplicationCommandLineArguments args)
		{
			this.Kp = args.PidKp ?? FromAppSettingsOrDefault("pid:kp", 0);
			this.Ki = args.PidKi ?? FromAppSettingsOrDefault("pid:ki", 0);
			this.Kd = args.PidKd ?? FromAppSettingsOrDefault("pid:kd", 0);
		}

		private static double FromAppSettingsOrDefault(string key, double defaultValue)
		{
			double value;
			if (double.TryParse(ConfigurationManager.AppSettings[key]?.Trim(), out value))
				return value;

			return defaultValue;
		}

		public double Kp { get; }

		public double Ki { get; }

		public double Kd { get; }
	}
}
