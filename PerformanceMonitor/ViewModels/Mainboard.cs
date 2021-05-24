using PerformanceMonitor.Business;
using WpfUtility.Services;

namespace PerformanceMonitor.ViewModels
{
    class Mainboard : ObservableObject
    {
        private string deviceName;
        public string DeviceName
        {
            get => deviceName;
            set
            {
                deviceName = value;
                OnPropertyChanged();
            }
        }

        public Mainboard()
        {
            DeviceName = Monitor.Instance.Mainboard.Name;
        }
    }
}
