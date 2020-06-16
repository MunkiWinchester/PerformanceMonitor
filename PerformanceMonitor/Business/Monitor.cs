using OpenHardwareMonitor.Hardware;
using PerformanceMonitor.DataObjetcs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PerformanceMonitor.Business
{
    public sealed class Monitor : IDisposable
    {
        private readonly Computer _computer;
        public Mainboard Mainboard { get; private set; }
        public CPU CPU { get; private set; }
        public RAM RAM { get; private set; }
        public GPU GPU { get; private set; }
        public List<HDD> HDDs { get; private set; } = new List<HDD>();

        private Monitor()
        {
            _computer = new Computer
            {
                CPUEnabled = true,
                GPUEnabled = true,
                RAMEnabled = true,
                MainboardEnabled = true,
                HDDEnabled = true,
                FanControllerEnabled = true
            };
            _computer.Open();

            foreach (IHardware hardware in _computer.Hardware)
            {
                if (hardware.Sensors.Any() || hardware.HardwareType ==  HardwareType.Mainboard)
                {
                    switch (hardware.HardwareType)
                    {
                        case HardwareType.Mainboard:
                            Mainboard = new Mainboard(hardware);
                            break;
                        case HardwareType.CPU:
                            CPU = new CPU(hardware);
                            break;
                        case HardwareType.RAM:
                            RAM = new RAM(hardware);
                            break;
                        case HardwareType.GpuNvidia:
                            GPU = new GPU(hardware);
                            break;
                        case HardwareType.HDD:
                            HDDs.Add(new HDD(hardware));
                            break;
                    }
                }
            }
        }

        public static Monitor Instance
        {
            get { return lazy.Value; }
        }

        private static readonly Lazy<Monitor> lazy = new Lazy<Monitor>(() => new Monitor());

        public void Dispose()
        {
            _computer.Close();
        }
    }
}
