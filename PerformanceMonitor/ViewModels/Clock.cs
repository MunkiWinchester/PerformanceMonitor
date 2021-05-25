using System;
using WpfUtility.Services;

namespace PerformanceMonitor.ViewModels
{
    class Clock : ObservableObject
    {
        private DateTime time;

        public DateTime Time
        {
            get { return time; }
            set
            {
                time = value;
                OnPropertyChanged();
            }
        }

        public Clock()
        {
            time = DateTime.Now;
            InitTimer(() => { Time = DateTime.Now; });
        }

        private System.Timers.Timer updateTimer;
        public void InitTimer(Action update)
        {
            updateTimer = new System.Timers.Timer();
            updateTimer.Elapsed += (sender, args) => update();
            updateTimer.Interval = TimeSpan.FromSeconds(5).TotalMilliseconds;
            updateTimer.Start();
        }
    }
}
