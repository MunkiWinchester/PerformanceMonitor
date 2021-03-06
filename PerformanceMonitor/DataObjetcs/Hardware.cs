﻿using LiveCharts;
using LiveCharts.Defaults;
using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using WpfUtility.Services;

namespace PerformanceMonitor.DataObjetcs
{
    public interface IUpdateable
    {
        void Update();
    }

    public abstract class ChartHardware : ObservableObject
    {
        public string Name => _hardware.Name;

        protected readonly IHardware _hardware;

        private System.Timers.Timer updateTimer;

        public ChartHardware(IHardware hardware)
        {
            _hardware = hardware;
        }

        public override string ToString()
        {
            return _hardware.Name;
        }

        public void InitTimer(Action update)
        {
            updateTimer = new System.Timers.Timer();
            updateTimer.Elapsed += (sender, args) => update();
            updateTimer.Interval = TimeSpan.FromSeconds(1).TotalMilliseconds; // in miliseconds
            updateTimer.Start();
        }
    }

    public class Mainboard : ChartHardware
    {
        public Mainboard(IHardware hardware) : base(hardware) { }
    }

    public class CORE
    {
        public string Name { get; private set; }
        public ChartValues<ObservableValue> Load { get; set; } = new ChartValues<ObservableValue>();
        public ChartValues<ObservableValue> Temperature { get; set; } = new ChartValues<ObservableValue>();
        public ChartValues<ObservableValue> Clock { get; set; } = new ChartValues<ObservableValue>();

        public CORE(string name)
        {
            Name = name;
        }
    }

    public class CPU : ChartHardware, IUpdateable
    {
        public ObservableCollection<CORE> Cores { get; set; } = new ObservableCollection<CORE>();
        public ChartValues<ObservableValue> Load { get; set; } = new ChartValues<ObservableValue>();
        public ChartValues<ObservableValue> Temperature { get; set; } = new ChartValues<ObservableValue>();
        public ChartValues<ObservableValue> Clock { get; set; } = new ChartValues<ObservableValue>();
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

        private readonly bool coresEnabled;
        private float currentLoad;
        private float currentTemp;

        public CPU(IHardware hardware, bool coresEnabled = false) : base(hardware)
        {
            this.coresEnabled = coresEnabled;
            if (this.coresEnabled)
            {
                int coreCount = 0;
                using (var processor = new System.Management.ManagementObjectSearcher("Select * from Win32_Processor"))
                {
                    foreach (var item in processor.Get())
                    {
                        coreCount += int.Parse(item["NumberOfCores"].ToString());
                    }
                }
                for (int i = 1; i <= coreCount; i++)
                    Cores.Add(new CORE($"CPU Core #{i}"));
            }

            Update();
            InitTimer(() => Update());
        }

        public void Update()
        {
            _hardware.Update();

            foreach (ISensor sensor in _hardware.Sensors)
            {
                switch (sensor.SensorType)
                {
                    case SensorType.Load:
                        if (sensor.Name == "CPU Total")
                            HardwareHelper.Queue(sensor.Value, Load);
                        CurrentLoad = sensor.Value ?? 0;
                        break;
                    case SensorType.Clock:
                        if (sensor.Name == "Bus Speed")
                            HardwareHelper.Queue(sensor.Value, Clock);
                        break;
                    case SensorType.Temperature:
                        if (sensor.Name == "CPU Package")
                        {
                            HardwareHelper.Queue(sensor.Value, Temperature);
                            CurrentTemp = sensor.Value ?? 0;
                        }
                        break;
                }
                if (coresEnabled)
                    UpdateCoreValues(sensor);
            }
        }

        private void UpdateCoreValues(ISensor sensor)
        {
            if (sensor.Name.ToLower().Contains("core"))
                HardwareHelper.Queueue(sensor.Name, sensor.Value, sensor.SensorType, Cores);
        }
    }

    public class RAM : ChartHardware, IUpdateable
    {
        public ChartValues<ObservableValue> Load { get; set; } = new ChartValues<ObservableValue>();
        public float MemoryUsed
        {
            get => memoryUsed;
            set
            {
                memoryUsed = value;
                OnPropertyChanged();
            }
        }
        public float MemoryAvailable
        {
            get => memoryAvailable;
            set
            {
                memoryAvailable = value;
                OnPropertyChanged();
            }
        }

        private float memoryUsed;
        private float memoryAvailable;

        public RAM(IHardware hardware) : base(hardware)
        {
            Update();
            InitTimer(() => Update());
        }

        public void Update()
        {
            _hardware.Update();

            foreach (ISensor sensor in _hardware.Sensors)
            {
                switch (sensor.SensorType)
                {
                    case SensorType.Load:
                        HardwareHelper.Queue(sensor.Value, Load);
                        break;
                    case SensorType.Data:
                        if (sensor.Name == "Used Memory")
                            MemoryUsed = sensor.Value ?? 0;
                        else if (sensor.Name == "Available Memory")
                            MemoryAvailable = sensor.Value ?? 0;
                        break;
                }
            }
        }
    }

    public class GPU : ChartHardware, IUpdateable
    {
        public ChartValues<ObservableValue> Load { get; set; } = new ChartValues<ObservableValue>();
        public ChartValues<ObservableValue> Temperature { get; set; } = new ChartValues<ObservableValue>();
        public ChartValues<ObservableValue> Clock { get; set; } = new ChartValues<ObservableValue>();
        public ChartValues<ObservableValue> Fan { get; set; } = new ChartValues<ObservableValue>();
        public ChartValues<ObservableValue> MemoryLoad { get; set; } = new ChartValues<ObservableValue>();
        public ChartValues<ObservableValue> MemoryClock { get; set; } = new ChartValues<ObservableValue>();
        public float MemoryUsed
        {
            get => memoryUsed;
            set
            {
                memoryUsed = value;
                OnPropertyChanged();
            }
        }
        public float MemoryAvailable
        {
            get => memoryAvailable;
            set
            {
                memoryAvailable = value;
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
        public float CurrentFan
        {
            get { return currentFan; }
            set
            {
                currentFan = value;
                OnPropertyChanged();
            }
        }

        private float memoryUsed;
        private float memoryAvailable;
        private float currentLoad;
        private float currentTemp;
        private float currentFan;

        public GPU(IHardware hardware) : base(hardware)
        {
            Update();
            InitTimer(() => Update());
        }

        public void Update()
        {
            _hardware.Update();

            foreach (ISensor sensor in _hardware.Sensors)
            {
                switch (sensor.SensorType)
                {
                    case SensorType.Load:
                        if (sensor.Name == "GPU Core")
                        {
                            HardwareHelper.Queue(sensor.Value, Load);
                            CurrentLoad = sensor.Value ?? 0;
                        }
                        else if (sensor.Name == "GPU Memory Total")
                            HardwareHelper.Queue(sensor.Value, MemoryLoad);
                        break;
                    case SensorType.SmallData:
                        if (sensor.Name == "GPU Memory Used")
                        {
                            MemoryUsed = sensor.Value ?? 0;
                            HardwareHelper.Queue(sensor.Value ?? 0, MemoryLoad);
                        }
                        else if (sensor.Name == "GPU Memory Total")
                            MemoryAvailable = sensor.Value ?? 0;
                        break;
                    case SensorType.Clock:
                        if (sensor.Name == "GPU Core")
                            HardwareHelper.Queue(sensor.Value, Clock);
                        else if (sensor.Name == "GPU Memory")
                            HardwareHelper.Queue(sensor.Value, MemoryClock);
                        break;
                    case SensorType.Fan:
                        HardwareHelper.Queue(sensor.Value, Fan);
                        CurrentFan = sensor.Value ?? 0;
                        break;
                    case SensorType.Temperature:
                        HardwareHelper.Queue(sensor.Value, Temperature);
                        CurrentTemp = sensor.Value ?? 0;
                        break;
                }
            }
        }
    }

    public class HDD : ChartHardware, IUpdateable
    {
        public float Load
        {
            get => load;
            set
            {
                load = value;
                OnPropertyChanged();
            }
        }
        public float Temperature
        {
            get => temperature;
            set
            {
                temperature = value;
                OnPropertyChanged();
            }
        }

        private float load;
        private float temperature;

        public HDD(IHardware hardware) : base(hardware)
        {
            Update();
            InitTimer(() => Update());
        }

        public void Update()
        {
            _hardware.Update();

            foreach (ISensor sensor in _hardware.Sensors)
            {
                switch (sensor.SensorType)
                {
                    case SensorType.Temperature:
                        Temperature = sensor.Value ?? 0;
                        break;
                    case SensorType.Load:
                        Load = sensor.Value ?? 0;
                        break;
                }
            }
        }
    }

    public static class HardwareHelper
    {
        public static void Queue(float? value, ChartValues<ObservableValue> values)
        {
            values.Add(new ObservableValue(Convert.ToDouble(value)));
            if (values.Count > 25)
                values.RemoveAt(0);
        }

        public static void Queueue(string name, float? value, SensorType type, ObservableCollection<CORE> cores)
        {
            var core = cores.FirstOrDefault(c => c.Name == name);
            switch (type)
            {
                case SensorType.Clock:
                    Queue(value, core.Clock);
                    break;
                case SensorType.Temperature:
                    Queue(value, core.Temperature);
                    break;
                case SensorType.Load:
                    Queue(value, core.Load);
                    break;
            }
        }
    }
}
