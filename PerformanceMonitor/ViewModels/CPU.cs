using LiveCharts;
using LiveCharts.Defaults;
using PerformanceMonitor.Business;
using WpfUtility.Services;

namespace PerformanceMonitor.ViewModels
{
    class CPU : ObservableObject
    {
        private string deviceName;
        private float currentLoad;
        private float currentTemp;

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

        public CPU()
        {
            DeviceName = Monitor.Instance.CPU.Name;
            Load = Monitor.Instance.CPU.Load;
            Clock = Monitor.Instance.CPU.Clock;
            Temperature = Monitor.Instance.CPU.Temperature;
            CurrentTemp = Monitor.Instance.CPU.CurrentTemp;
            CurrentLoad = Monitor.Instance.CPU.CurrentLoad;

            Monitor.Instance.CPU.PropertyChanged += CPU_PropertyChanged;
        }

        private void CPU_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(DataObjetcs.CPU.CurrentLoad):
                    CurrentLoad = Monitor.Instance.CPU.CurrentLoad;
                    break;
                case nameof(DataObjetcs.CPU.CurrentTemp):
                    CurrentTemp = Monitor.Instance.CPU.CurrentTemp;
                    break;
            }
        }
    }
}
