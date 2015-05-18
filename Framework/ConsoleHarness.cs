using System;

namespace BlackRabbit.WindowsService.DebuggableService.Framework
{
    /// <summary>
    /// The harness for controlling service output to the console
    /// </summary>
	public static class ConsoleHarness
	{
        /// <summary>
        /// Run a service from the console given a service implementation
        /// </summary>
        /// <param name="args">The arguments from the command line prompt</param>
        /// <param name="service">The implementation of the windows service interface</param>
        public static void Run(string[] args, IWindowsService service)
		{
			string serviceName = service.GetType().Name;
			bool isRunning = true;

			// simulate starting the windows service
			service.OnStart(args);

			// let it run as long as Q is not pressed
			while (isRunning)
			{
				WriteToConsole(ConsoleColor.Yellow, "Enter either [Q]uit, [P]ause, [R]esume : ");
				isRunning = HandleConsoleInput(service, Console.ReadLine());
			}

			// stop and shutdown
			service.OnStop();
			service.OnShutdown();
		}

		// Private input handler for console commands.
		private static bool HandleConsoleInput(IWindowsService service, string line)
		{
			bool canContinue = true;

			// check input
			if (line != null)
			{
				switch (line.ToUpper())
				{
					case "Q":
						canContinue = false;
						break;

					case "P":
						service.OnPause();
						break;

					case "R":
						service.OnContinue();
						break;

					default:
						WriteToConsole(ConsoleColor.Red, "Did not understand that input, try again.");
						break;
				}
			}

			return canContinue;
		}

		/// <summary>
        /// Outputs a line of text to the console
        /// </summary>
        /// <param name="foregroundColor">The color to use</param>
        /// <param name="text">The text to print</param>
		public static void WriteToConsole(ConsoleColor foregroundColor, string text)
		{
			ConsoleColor originalColor = Console.ForegroundColor;
			Console.ForegroundColor = foregroundColor;

			Console.WriteLine(text);
			Console.Out.Flush();

			Console.ForegroundColor = originalColor;
		}
	}
}
