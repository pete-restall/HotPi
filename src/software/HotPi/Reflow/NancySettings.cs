using System;
using System.Configuration;
using Restall.HotPi.Nancy;

namespace Restall.HotPi.Reflow
{
	public class NancySettings : IHaveNancySettings
	{
		public NancySettings(ReflowControllerApplicationCommandLineArguments args)
		{
			this.Host = new Uri(
				args.UiHost != string.Empty
					? args.UiHost
					: FromAppSettingsOrDefault("ui:nancy/Host", "http://localhost/"),
				UriKind.Absolute);
		}

		private static string FromAppSettingsOrDefault(string key, string defaultValue)
		{
			return ConfigurationManager.AppSettings[key]?.Trim() ?? defaultValue;
		}

		public Uri Host { get; }
	}
}
