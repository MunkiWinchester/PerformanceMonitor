using PerformanceMonitor.Business;
using System.Windows.Controls;

namespace PerformanceMonitor.Views.Hardware
{
    /// <summary>
    /// Interaction logic for Mainboard.xaml
    /// </summary>
    public partial class Mainboard : UserControl
    {
        public string DeviceName { get; set; }

        public Mainboard()
        {
            InitializeComponent();

            DeviceName = Monitor.Instance.Mainboard.Name;
        }
    }
}
