using LiveCharts;
using LiveCharts.Defaults;
using PerformanceMonitor.Business;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PerformanceMonitor.Views.Hardware
{
    /// <summary>
    /// Interaction logic for GPU.xaml
    /// </summary>
    public partial class GPU : UserControl
    {
        public ChartValues<ObservableValue> Load { get; set; }
        public ChartValues<ObservableValue> Clock { get; set; }
        public ChartValues<ObservableValue> Temperature { get; set; }
        public ChartValues<ObservableValue> Fan { get; set; }
        public ChartValues<ObservableValue> Ram { get; set; }
        public float RamMax { get; set; }

        public GPU()
        {
            InitializeComponent();

            Load = Monitor.Instance.GPU.Load;
            Clock = Monitor.Instance.GPU.Clock;
            Temperature = Monitor.Instance.GPU.Temperature;
            Fan = Monitor.Instance.GPU.Fan;
            Ram = Monitor.Instance.GPU.MemoryLoad;
            RamMax = Monitor.Instance.GPU.MemoryAvailable;
        }
    }
}
