﻿using MahApps.Metro.Controls;
using System;
using System.Windows;
using System.Windows.Input;
using WpfAppBar;
using static PerformanceMonitor.Business.Helper;

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

        /// <summary>
        /// DependencyProperty for the progress bar color
        /// </summary>
        public static readonly DependencyProperty AlwaysTopMostProperty = DependencyProperty.Register(
            nameof(AlwaysTopMost), typeof(bool), typeof(MetroWindow),
            new PropertyMetadata(true, AlwaysTopMostPropertyChangedCallback));

        /// <summary>
        /// Value of the top label
        /// </summary>
        public bool AlwaysTopMost
        {
            get => (bool)GetValue(AlwaysTopMostProperty);
            set => SetValue(AlwaysTopMostProperty, value);
        }

        public MainWindow()
        {
            InitializeComponent();

            Left = SystemParameters.VirtualScreenLeft;
            Top = SystemParameters.VirtualScreenHeight - Math.Abs(SystemParameters.VirtualScreenTop) - Height;

            var position = GetTaskBarLocation();
            if (position == TaskBarLocation.BOTTOM)
            {
                Top += SystemParameters.WorkArea.Bottom;
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
                AppBarFunctions.SetAppBar(this, ABEdge.Bottom);
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

        private static void AlwaysTopMostPropertyChangedCallback(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (dependencyObject is MetroWindow mainWindow)
            {
                mainWindow.Topmost = (bool)dependencyPropertyChangedEventArgs.NewValue;
            }
        }
    }
}
