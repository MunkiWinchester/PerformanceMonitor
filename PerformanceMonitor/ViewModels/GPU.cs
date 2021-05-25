using LiveCharts;
using LiveCharts.Defaults;
using PerformanceMonitor.Business;
using System;
using WpfUtility.Services;

namespace PerformanceMonitor.ViewModels
{
    class GPU : ObservableObject
    {
        private float ram;
        private float ramMax;
        private string deviceName;
        private Func<double, string> formatter;

        public ChartValues<ObservableValue> Load { get; set; }
        public ChartValues<ObservableValue> Clock { get; set; }
        public ChartValues<ObservableValue> Temperature { get; set; }
        public ChartValues<ObservableValue> Fan { get; set; }
        //public ChartValues<ObservableValue> Ram { get; set; }
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

        public GPU()
        {
            Formatter = value => Helper.FormatRamLabel(value);

            DeviceName = Monitor.Instance.GPU.Name?.Replace("NVIDIA ", "");
            Load = Monitor.Instance.GPU.Load;
            Clock = Monitor.Instance.GPU.Clock;
            Temperature = Monitor.Instance.GPU.Temperature;
            Fan = Monitor.Instance.GPU.Fan;
            Ram = Monitor.Instance.GPU.MemoryUsed;
            RamMax = Monitor.Instance.GPU.MemoryAvailable;

            Monitor.Instance.GPU.PropertyChanged += GPU_PropertyChanged;
        }

        private void GPU_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(DataObjetcs.GPU.MemoryUsed):
                    Ram = Monitor.Instance.GPU.MemoryUsed;
                    break;
                case nameof(DataObjetcs.GPU.MemoryAvailable):
                    RamMax = Monitor.Instance.GPU.MemoryAvailable;
                    break;
            }
        }
    }
}
