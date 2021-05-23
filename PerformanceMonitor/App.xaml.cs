using PerformanceMonitor.Business;
using System.Windows;

namespace PerformanceMonitor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Monitor.Instance.Dispose();
        }
    }
}
