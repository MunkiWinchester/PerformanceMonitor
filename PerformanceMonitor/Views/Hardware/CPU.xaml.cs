using System.Windows;
using System.Windows.Controls;

namespace PerformanceMonitor.Views.Hardware
{
    /// <summary>
    /// Interaction logic for CPU.xaml
    /// </summary>
    public partial class CPU : UserControl
    {
        /// <summary>
        /// DependencyProperty for the progress bar color
        /// </summary>
        public static readonly DependencyProperty IsInDesignModeProperty = DependencyProperty.Register(
            nameof(IsInDesignMode), typeof(bool), typeof(CPU),
            new PropertyMetadata(false));

        /// <summary>
        /// Value of the top label
        /// </summary>
        public bool IsInDesignMode
        {
            get => (bool)GetValue(IsInDesignModeProperty);
            set => SetValue(IsInDesignModeProperty, value);
        }

        public CPU()
        {
            InitializeComponent();
        }
    }
}
