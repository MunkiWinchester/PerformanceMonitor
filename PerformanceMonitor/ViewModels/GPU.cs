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
        private float currentLoad;
        private float currentTemp;
        private float currentFan;

        public ChartValues<ObservableValue> Load { get; set; }
        public ChartValues<ObservableValue> Clock { get; set; }
        public ChartValues<ObservableValue> Temperature { get; set; }
        public ChartValues<ObservableValue> Fan { get; set; }
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
        public float CurrentLoad
        {
            get { return currentLoad; }
            set
            {
                currentLoad = value;
                OnPropertyChanged();
            }
        }
        public float CurrentTemp
        {
            get { return currentTemp; }
            set
            {
                currentTemp = value;
                OnPropertyChanged();
            }
        }
        public float CurrentFan
        {
            get { return currentFan; }
            set
            {
                currentFan = value;
                OnPropertyChanged();
            }
        }

        public GPU()
        {
            DeviceName = Monitor.Instance.GPU.Name?.Replace("NVIDIA ", "");
            Load = Monitor.Instance.GPU.Load;
            Clock = Monitor.Instance.GPU.Clock;
            Temperature = Monitor.Instance.GPU.Temperature;
            Fan = Monitor.Instance.GPU.Fan;
            Ram = Monitor.Instance.GPU.MemoryUsed;
            RamMax = Monitor.Instance.GPU.MemoryAvailable;
            CurrentTemp = Monitor.Instance.GPU.CurrentTemp;
            CurrentLoad = Monitor.Instance.GPU.CurrentLoad;
            CurrentFan = Monitor.Instance.GPU.CurrentFan;

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
                case nameof(DataObjetcs.GPU.CurrentLoad):
                    CurrentLoad = Monitor.Instance.GPU.CurrentLoad;
                    break;
                case nameof(DataObjetcs.GPU.CurrentTemp):
                    CurrentTemp = Monitor.Instance.GPU.CurrentTemp;
                    break;
                case nameof(DataObjetcs.GPU.CurrentFan):
                    CurrentFan = Monitor.Instance.GPU.CurrentFan;
                    break;
            }
        }
    }
}
