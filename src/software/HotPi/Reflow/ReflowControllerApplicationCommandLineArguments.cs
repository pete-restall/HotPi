using CommandLine;

namespace Restall.HotPi.Reflow
{
	[Verb("reflow", HelpText = "Main Reflow Application")]
	public class ReflowControllerApplicationCommandLineArguments
	{
		public ReflowControllerApplicationCommandLineArguments(string uiHost, decimal? max31850ColdJunctionStaticErrorInCelsius)
		{
			this.UiHost = uiHost;
			this.Max31850ColdJunctionStaticErrorInCelsius = max31850ColdJunctionStaticErrorInCelsius;
		}

		[Option(longName: "ui-host", shortName: 'u', Default = "", HelpText = "The URL that will host the UI")]
		public string UiHost { get; }

		[Option(longName: "max31850-cjc-error-celsius", Default = null, HelpText = "The static error, in Celsius, of the MAX31850 Cold Junction Temperature sensor")]
		public decimal? Max31850ColdJunctionStaticErrorInCelsius { get; }
	}
}
