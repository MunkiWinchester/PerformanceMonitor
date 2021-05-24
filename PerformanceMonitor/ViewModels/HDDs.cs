using PerformanceMonitor.Business;
using System.Collections.ObjectModel;
using WpfUtility.Services;

namespace PerformanceMonitor.ViewModels
{
    class HDDs : ObservableObject
    {
        private ObservableCollection<DataObjetcs.HDD> drives;
        public ObservableCollection<DataObjetcs.HDD> Drives
        {
            get => drives;
            set => SetField(ref drives, value);
        }

        public HDDs()
        {
            Drives = new ObservableCollection<DataObjetcs.HDD>(Monitor.Instance.HDDs);
        }
    }
}
