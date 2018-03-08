using System;
using System.Collections.Generic;
using System.IO;
using CommandLine;
using Restall.HotPi.Reflow;

namespace Restall.HotPi
{
	public static class Program
	{
		private static TextWriter Stderr => Console.Error;

		public static int Main(string[] args)
		{
			using (var parser = new Parser(cfg =>
			{
				cfg.CaseSensitive = true;
				cfg.HelpWriter = Stderr;
				cfg.IgnoreUnknownArguments = false;
				cfg.MaximumDisplayWidth = Console.WindowWidth - 2;
			}))
			{
				return parser
					.ParseArguments<ReflowControllerApplicationCommandLineArguments>(args)
					.MapResult(ApplicationBootstrapper.Run<ReflowControllerApplication, ReflowControllerApplicationCommandLineArguments>, CommandLineError);
			}
		}

		private static int CommandLineError(IEnumerable<Error> errors)
		{
			Stderr.WriteLine("Unable to parse command-line arguments.");
			errors.ForEach(error => Console.WriteLine("\t{0}", error));

			// TODO: NEED TO ADD THE USAGE TEXT AS WELL...
			return 1;
		}
	}
}
