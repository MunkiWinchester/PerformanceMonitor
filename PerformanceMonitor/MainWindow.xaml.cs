using System;
using System.Windows;
using OpenHardwareMonitor.Hardware;

namespace PerformanceMonitor
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();

		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			Computer computer = new Computer
			{
				CPUEnabled = true,
				GPUEnabled = true,
				RAMEnabled = true,
				MainboardEnabled = true,
				HDDEnabled = true,
				FanControllerEnabled = true
			};
			computer.Open();

			NewMethod(computer.Hardware);
		}

		private void NewMethod(IHardware[] hardwareArray)
		{
			foreach (IHardware hardware in hardwareArray)
			{
				hardware.Update();
				textBox.Text += $"\n{hardware.HardwareType}: { hardware.Name}\n";
				foreach (ISensor sensor in hardware.Sensors)
				{
					textBox.Text += $"\t\"{sensor.SensorType}\"\t \"{sensor.Name}\"\t {sensor.Value}\n";
				}
				Console.WriteLine();
			}
		}
	}
}
