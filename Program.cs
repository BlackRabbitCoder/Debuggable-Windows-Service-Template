using System;
using System.Linq;
using System.ServiceProcess;
using BlackRabbit.WindowsService.DebuggableService.Framework;


namespace BlackRabbit.WindowsService.DebuggableService
{
	static class Program
	{
		// The main entry point for the windows service application.
		static void Main(string[] args)
		{
			try
			{
				// if install was a command line flag, then run the installer at runtime.
				if (args.Contains("-install", StringComparer.InvariantCultureIgnoreCase))
				{
					WindowsServiceInstaller.RuntimeInstall<ServiceImplementation>();
				}
				else if (args.Contains("-uninstall", StringComparer.InvariantCultureIgnoreCase))
				{
                    // if uninstall was a command line flag, run uninstaller at runtime.
                    WindowsServiceInstaller.RuntimeUnInstall<ServiceImplementation>();
				}
				else
				{
                    // otherwise, fire up the service as either console or windows service based on UserInteractive property.
                    var implementation = new ServiceImplementation();

					// if started from console, file explorer, etc, run as console app.
					if (Environment.UserInteractive)
					{
						ConsoleHarness.Run(args, implementation);
					}
					else
					{
                        // otherwise run as a windows service
                        ServiceBase.Run(new WindowsServiceHarness(implementation));
					}
				}
			}
			catch (Exception ex)
			{
				ConsoleHarness.WriteToConsole(ConsoleColor.Red, $"An exception occurred in Main(): {ex}");
			}
		}
	}
}
