using LiveCharts;
using LiveCharts.Defaults;
using PerformanceMonitor.Business;
using System;
using WpfUtility.Services;

namespace PerformanceMonitor.ViewModels
{
    class RAM : ObservableObject
    {
        private float ram;
        private float ramMax;
        private string deviceName;
        private Func<double, string> formatter;

        public ChartValues<ObservableValue> Load { get; set; }
        public float Ram
        {
            get => ram;
            set
            {
                ram = value;
                OnPropertyChanged();
            }
        }
        public float RamMax
        {
            get => ramMax;
            set
            {
                ramMax = value;
                OnPropertyChanged();
            }
        }
        public string DeviceName
        {
            get => deviceName;
            set
            {
                deviceName = value;
                OnPropertyChanged();
            }
        }

        public Func<double, string> Formatter
        {
            get => formatter;
            set
            {
                formatter = value;
                OnPropertyChanged();
            }
        }

        public RAM()
        {
            Formatter = value => Helper.FormatRamLabel(value);
            DeviceName = Monitor.Instance.RAM.Name;
            Load = Monitor.Instance.RAM.Load;
            Ram = Monitor.Instance.RAM.MemoryUsed;
            RamMax = Monitor.Instance.RAM.MemoryAvailable + Monitor.Instance.RAM.MemoryUsed;
        }
    }
}
