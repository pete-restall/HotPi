using CommandLine;

namespace Restall.HotPi.Reflow
{
	[Verb("reflow", HelpText = "Main Reflow Application")]
	public class ReflowControllerApplicationCommandLineArguments
	{
		public ReflowControllerApplicationCommandLineArguments(string uiHost)
		{
			this.UiHost = uiHost;
		}

		[Option(longName: "ui-host", shortName: 'u', Default = "", HelpText = "The URL that will host the UI")]
		public string UiHost { get; }
	}
}
