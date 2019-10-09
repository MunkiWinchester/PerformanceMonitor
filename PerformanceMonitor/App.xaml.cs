using System;
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
			Console.WriteLine(Monitor.Instance.Mainboard.Name);
		}
	}
}
