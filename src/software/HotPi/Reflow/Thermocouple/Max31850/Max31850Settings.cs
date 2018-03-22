using System.Configuration;

namespace Restall.HotPi.Reflow.Thermocouple.Max31850
{
	public class Max31850Settings : IHaveMax31850Settings
	{
		public Max31850Settings(ReflowControllerApplicationCommandLineArguments args)
		{
			this.ColdJunctionStaticErrorInCelsius = WithinOrDefault(
				minimumValue: -2,
				maximumValue: 2,
				defaultValue: 0,
				value: args.Max31850ColdJunctionStaticErrorInCelsius ?? FromAppSettingsOrDefault("max31850:cjcStaticErrorInCelsius", 0));
		}

		private static decimal WithinOrDefault(decimal minimumValue, decimal maximumValue, decimal defaultValue, decimal value)
		{
			if (value < minimumValue || value > maximumValue)
				return defaultValue;

			return value;
		}

		private static decimal FromAppSettingsOrDefault(string key, decimal defaultValue)
		{
			decimal value;
			if (!decimal.TryParse(ConfigurationManager.AppSettings[key]?.Trim(), out value))
				return defaultValue;

			return value;
		}

		public decimal ColdJunctionStaticErrorInCelsius { get; }
	}
}
