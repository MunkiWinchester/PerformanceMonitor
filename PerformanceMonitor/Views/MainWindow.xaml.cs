using LiveCharts.Configurations;
using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Input;
using static PerformanceMonitor.Business.Helper;
using WpfAppBar;

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

        /// <summary>
        /// DependencyProperty for the progress bar color
        /// </summary>
        public static readonly DependencyProperty IsInNormalModeProperty = DependencyProperty.Register(
            nameof(IsInNormalMode), typeof(bool), typeof(MetroWindow),
            new PropertyMetadata(true));

        /// <summary>
        /// Value of the top label
        /// </summary>
        public bool IsInNormalMode
        {
            get => (bool)GetValue(IsInNormalModeProperty);
            set => SetValue(IsInNormalModeProperty, value);
        }

        public MainWindow()
        {
            InitializeComponent();


            Left = SystemParameters.VirtualScreenLeft;
            Top = SystemParameters.VirtualScreenTop;

            var position = GetTaskBarLocation();
            if (position == TaskBarLocation.TOP)
            {
                Top = SystemParameters.VirtualScreenTop + SystemParameters.WorkArea.Top;
            }
        }

        private void MetroWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            SetValue(IsInDesignModeProperty, !IsInDesignMode);
        }

        private void MenuItem_AppBar_Click(object sender, RoutedEventArgs e)
        {
            SetValue(IsInNormalModeProperty, !IsInNormalMode);
            if (!IsInNormalMode)
            {
                AppBarFunctions.SetAppBar(this, ABEdge.Left);
            }
            else
            {
                AppBarFunctions.SetAppBar(this, ABEdge.None);
            }
        }

        private void MenuItem_Close_Click(object sender, RoutedEventArgs e)
        {
            if (!IsInNormalMode)
            {
                AppBarFunctions.SetAppBar(this, ABEdge.None);
            }
            Close();
        }
    }
}
