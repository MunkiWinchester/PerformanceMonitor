using System.Windows;

namespace PerformanceMonitor.Business
{
    public static class Helper
    {
        public enum TaskBarLocation { TOP, BOTTOM, LEFT, RIGHT }

        public static TaskBarLocation GetTaskBarLocation()
        {
            TaskBarLocation taskBarLocation = TaskBarLocation.BOTTOM;
            bool taskBarOnTopOrBottom = (SystemParameters.WorkArea.Width == SystemParameters.PrimaryScreenWidth);
            if (taskBarOnTopOrBottom)
            {
                if (SystemParameters.WorkArea.Top > 0)
                    taskBarLocation = TaskBarLocation.TOP;
            }
            else
            {
                if (SystemParameters.WorkArea.Left > 0)
                {
                    taskBarLocation = TaskBarLocation.LEFT;
                }
                else
                {
                    taskBarLocation = TaskBarLocation.RIGHT;
                }
            }
            return taskBarLocation;
        }
    }
}
