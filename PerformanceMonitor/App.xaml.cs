﻿using PerformanceMonitor.Business;
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
            //var clock = Monitor.Instance.CPU.Clock;
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            //Monitor.Instance.Dispose();
        }
    }
}
