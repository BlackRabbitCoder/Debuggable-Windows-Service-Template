using System.ServiceProcess;

namespace HareBrained.WindowsService.Framework
{
    /// <summary>
    /// Handles the execution of a windows service using the Windows Service Manager
    /// </summary>
    public static class WindowsServiceMain
    {
        /// <summary>
        /// Runs the service implementation as a service (instead of console app)
        /// </summary>
        /// <param name="service">The implementation of the service.</param>
        public static void Run(IWindowsService service)
        {
            using (var serviceHarness = new WindowsServiceHarness(service))
            {
                ServiceBase.Run(serviceHarness);
            }
        }
    }
}
