using LiveCharts;
using LiveCharts.Defaults;
using PerformanceMonitor.Business;
using WpfUtility.Services;

namespace PerformanceMonitor.ViewModels
{
    class CPU : ObservableObject
    {
        private string deviceName;
        public ChartValues<ObservableValue> Load { get; set; }
        public ChartValues<ObservableValue> Clock { get; set; }
        public ChartValues<ObservableValue> Temperature { get; set; }
        public string DeviceName
        {
            get => deviceName;
            set
            {
                deviceName = value;
                OnPropertyChanged();
            }
        }

        public CPU()
        {
            DeviceName = Monitor.Instance.CPU.Name;
            Load = Monitor.Instance.CPU.Load;
            Clock = Monitor.Instance.CPU.Clock;
            Temperature = Monitor.Instance.CPU.Temperature;
        }
    }
}
