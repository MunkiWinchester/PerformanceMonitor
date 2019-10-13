using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Input;

namespace PerformanceMonitor.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// DependencyProperty for the progress bar color
        /// </summary>
        public static readonly DependencyProperty IsInDesignModeProperty = DependencyProperty.Register(
            nameof(IsInDesignMode), typeof(bool), typeof(MetroWindow),
            new PropertyMetadata(false));

        /// <summary>
        /// Value of the top label
        /// </summary>
        public bool IsInDesignMode
        {
            get => (bool)GetValue(IsInDesignModeProperty);
            set => SetValue(IsInDesignModeProperty, value);
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void MenuItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SetValue(IsInDesignModeProperty, !IsInDesignMode);
        }
    }
}
