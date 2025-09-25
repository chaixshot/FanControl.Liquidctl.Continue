using System;
using System.Collections.Generic;
using System.Linq;
using FanControl.Plugins;

namespace FanControl.Liquidctl
{
    internal class LiquidctlDevice
    {
        public class LiquidTemperature : IPluginSensor
        {
            public LiquidTemperature(LiquidctlStatusJSON output)
            {
                _id = $"{output.address}-liqtmp";
                _name = $"Liquid Temp. - {output.description}";
                UpdateFromJSON(output);
            }

            public void UpdateFromJSON(LiquidctlStatusJSON output)
            {
                _value = (float)output.status.Single(entry => entry.key == KEY).value;
            }

            public static readonly string KEY = "Liquid temperature";
            public string Id => _id;
            string _id;

            public string Name => _name;
            string _name;

            public float? Value => _value;
            float _value;

            public void Update()
            { } // plugin updates sensors
        }

        public class PumpSpeed : IPluginSensor
        {
            public PumpSpeed(LiquidctlStatusJSON output)
            {
                _id = $"{output.address}-pumprpm";
                _name = $"Pump - {output.description}";
                UpdateFromJSON(output);
            }

            public void UpdateFromJSON(LiquidctlStatusJSON output)
            {
                _value = (float)output.status.Single(entry => entry.key == KEY).value;
            }

            public static readonly string KEY = "Pump speed";
            public string Id => _id;
            readonly string _id;

            public string Name => _name;
            readonly string _name;

            public float? Value => _value;
            float _value;

            public void Update()
            { } // plugin updates sensors
        }

        public class PumpDuty : IPluginControlSensor
        {
            public PumpDuty(LiquidctlStatusJSON output)
            {
                _address = output.address;
                _id = $"{_address}-pumpduty";
                _name = $"Pump Control - {output.description}";
                UpdateFromJSON(output);
            }

            public void UpdateFromJSON(LiquidctlStatusJSON output)
            {
                _value = (float)output.status.Single(entry => entry.key == KEY).value;
            }

            public static readonly string KEY = "Pump duty";

            static readonly int MAX_RPM = 2800;

            public string Id => _id;
            string _id;
            string _address;

            public string Name => _name;
            string _name;

            public float? Value => _value;
            float _value;

            public void Reset()
            {
                Set(60.0f);
            }

            public void Set(float val)
            {
                LiquidctlCLIWrapper.SetPump(_address, (int) val);
            }

            public void Update()
            { } // plugin updates sensors

        }

        public class FanSpeed : IPluginSensor
        {
            public FanSpeed(LiquidctlStatusJSON output)
            {
                _id = $"{output.address}-fanrpm";
                _name = $"Fan - {output.description}";
                UpdateFromJSON(output);
            }

            public void UpdateFromJSON(LiquidctlStatusJSON output)
            {
                _value = (float)output.status.Single(entry => entry.key == KEY).value;
            }

            public static readonly string KEY = "Fan speed";
            public string Id => _id;
            readonly string _id;

            public string Name => _name;
            readonly string _name;

            public float? Value => _value;
            float _value;

            public void Update()
            { } // plugin updates sensors
        }
        
        public class FanDuty : IPluginControlSensor
        {
            public FanDuty(LiquidctlStatusJSON output)
            {
                _address = output.address;
                _id = $"{output.address}-fanctrl";
                _name = $"Fan Control - {output.description}";
                UpdateFromJSON(output);
            }

            // We can only estimate, as it is not provided in any output
            public void UpdateFromJSON(LiquidctlStatusJSON output)
            {
                _value = (float)output.status.Single(entry => entry.key == KEY).value;
            }

            public static readonly string KEY = "Fan duty";

            static readonly int MAX_RPM = 1980;

            public string Id => _id;
            readonly string _id;
            string _address;

            public string Name => _name;
            readonly string _name;

            public float? Value => _value;
            float _value;

            public void Reset()
            {
                Set(50.0f);
            }

            public void Set(float val)
            {
                LiquidctlCLIWrapper.SetFan(_address, (int) val);
            }

            public void Update()
            { } // plugin updates sensors
        }

        public class MicroFanSpeed : IPluginSensor
        {
            public MicroFanSpeed(LiquidctlStatusJSON output)
            {
                _id = $"{output.address}-fanrpm";
                _name = $"Micro fan - {output.description}";
                UpdateFromJSON(output);
            }

            public void UpdateFromJSON(LiquidctlStatusJSON output)
            {
                _value = (float)output.status.Single(entry => entry.key == KEY).value;
            }

            public static readonly string KEY = "Pump fan speed";
            public string Id => _id;
            readonly string _id;

            public string Name => _name;
            readonly string _name;

            public float? Value => _value;
            float _value;

            public void Update()
            { } // plugin updates sensors
        }

        public class MicroFanDuty : IPluginControlSensor
        {
            public MicroFanDuty(LiquidctlStatusJSON output)
            {
                _address = output.address;
                _id = $"{output.address}-fanctrl";
                _name = $"Micro fan Control - {output.description}";
                UpdateFromJSON(output);
            }

            // We can only estimate, as it is not provided in any output
            public void UpdateFromJSON(LiquidctlStatusJSON output)
            {
                _value = (float)output.status.Single(entry => entry.key == KEY).value;
            }

            public static readonly string KEY = "Pump fan duty";

            static readonly int MAX_RPM = 5200;

            public string Id => _id;
            readonly string _id;
            string _address;

            public string Name => _name;
            readonly string _name;

            public float? Value => _value;
            float _value;

            public void Reset()
            {
                Set(50.0f);
            }

            public void Set(float val)
            {
                LiquidctlCLIWrapper.SetMicroFan(_address, (int)val);
            }

            public void Update()
            { } // plugin updates sensors
        }

        // Try to get the speeds for multiple fans
        public class FanSpeedMultiple : IPluginSensor
        {
            public FanSpeedMultiple(int index, LiquidctlStatusJSON output)
            {
                _id = $"{output.address}-fan{index}rpm";
                _name = $"Fan {index} - {output.description}";
                UpdateFromJSON(index, output);
            }

            public void UpdateFromJSON(int index, LiquidctlStatusJSON output)
            {
                string currentKey = KEY.Replace("###", index.ToString());
                _value = (float) output.status.Single(entry => entry.key == currentKey).value;
            }

            public static string KEY = "Fan ### speed";

            public string Id => _id;
            readonly string _id;

            public string Name => _name;
            readonly string _name;

            public float? Value => _value;
            float _value;

            public void Update() { } // plugin updates sensors
        }

        // Try to control multiple fans
        public class FanDutyMultiple : IPluginControlSensor
        {
            public FanDutyMultiple(int index, LiquidctlStatusJSON output)
            {
                _address = output.address;
                _id = $"{output.address}-fan{index}ctrl";
                _name = $"Fan {index} Control - {output.description}";
                _index = index;

                UpdateFromJSON(index, output);
            }

            // We can only estimate, as it is not provided in any output
            public void UpdateFromJSON(int index, LiquidctlStatusJSON output) {
                string currentKey = FanSpeedMultiple.KEY.Replace("###", index.ToString());
                _value = (float)output.status.Single(entry => entry.key == currentKey).value;
            }

            public static string KEY = "Fan ### dufy";
            //public static string KEY = $"Fan {_index} speed";

            static readonly int MAX_RPM = 1980;

            public string Id => _id;
            readonly string _id;
            string _address;

            public string Name => _name;
            readonly string _name;

            public float? Value => _value;
            float _value;

            public int Index => _index;
            int _index;

            public void Reset()
            {
                Set(50.0f);
            }

            public void Set(float val)
            {
                LiquidctlCLIWrapper.SetFanNumber(_address, _index, (int) val);
            }

            public void Update() { } // plugin updates sensors
        }

        public LiquidctlDevice(LiquidctlStatusJSON output, IPluginLogger pluginLogger)
        {
            logger = pluginLogger;
            address = output.address;

            hasPumpSpeed = output.status.Exists(entry => entry.key == PumpSpeed.KEY && !(entry.value is null));
            if (hasPumpSpeed) {
                pumpSpeed = new PumpSpeed(output);
            }

            hasPumpDuty = output.status.Exists(entry => entry.key == PumpDuty.KEY && !(entry.value is null));
            if (hasPumpDuty) {
                pumpDuty = new PumpDuty(output);
            }

            hasFanSpeed = output.status.Exists(entry => entry.key == FanSpeed.KEY && !(entry.value is null));
            if (hasFanSpeed) {
                fanSpeed = new FanSpeed(output);
                fanControl = new FanDuty(output);
            }

            hasMicroFanSpeed = output.status.Exists(entry => entry.key == MicroFanSpeed.KEY && !(entry.value is null));
            if (hasMicroFanSpeed) {
                microFanSpeed = new MicroFanSpeed(output);
                microFanControl = new MicroFanDuty(output);
            }

            hasLiquidTemperature = output.status.Exists(entry => entry.key == LiquidTemperature.KEY && !(entry.value is null));
            if (hasLiquidTemperature) {
                liquidTemperature = new LiquidTemperature(output);
            }

            
            // Get the info for multiple fans
            for (int i=0; i<20; i++) {
                int index = i+1;
                string currentKey = FanSpeedMultiple.KEY.Replace("###", index.ToString());
                hasMultipleFanSpeed[i] = output.status.Exists(entry => entry.key == currentKey && !(entry.value is null));

                if (hasMultipleFanSpeed[i]) {
                    fanSpeedMultiple[i] = new FanSpeedMultiple(index, output);
                    fanControlMultiple[i] = new FanDutyMultiple(index, output);
                }
            }
        }


        public readonly bool hasPumpSpeed, hasPumpDuty, hasLiquidTemperature, hasFanSpeed, hasMicroFanSpeed;
        public readonly bool[] hasMultipleFanSpeed = new bool[20];


        public void UpdateFromJSON(LiquidctlStatusJSON output)
        {
            if (hasLiquidTemperature) liquidTemperature.UpdateFromJSON(output);
            if (hasPumpSpeed) pumpSpeed.UpdateFromJSON(output);
            if (hasPumpDuty) pumpDuty.UpdateFromJSON(output);
            if (hasFanSpeed) {
                fanSpeed.UpdateFromJSON(output);
                fanControl.UpdateFromJSON(output);
            }
            if (hasMicroFanSpeed) {
                microFanSpeed.UpdateFromJSON(output);
                microFanControl.UpdateFromJSON(output);
            }

            for (int i = 0; i<20; i++) {
                if (hasMultipleFanSpeed[i]) {
                    fanSpeedMultiple[i].UpdateFromJSON(i+1, output);
                    fanControlMultiple[i].UpdateFromJSON(i+1, output);
                }
            }
        }


        internal IPluginLogger logger;
        public string address;
        public LiquidTemperature liquidTemperature;
        public PumpSpeed pumpSpeed;
        public PumpDuty pumpDuty;
        public FanSpeed fanSpeed;
        public FanDuty fanControl;
        public MicroFanSpeed microFanSpeed;
        public MicroFanDuty microFanControl;

        public FanSpeedMultiple[] fanSpeedMultiple = new FanSpeedMultiple[20];
        public FanDutyMultiple[] fanControlMultiple = new FanDutyMultiple[20];


        public void LoadJSON()
        {
            try
            {
                LiquidctlStatusJSON output = LiquidctlCLIWrapper.ReadStatus(address).First();
                UpdateFromJSON(output);
            }
            catch (InvalidOperationException)
            {
                throw new Exception($"Device {address} not showing up");
            }
        }


        public String GetDeviceInfo() {
            String ret = $"Device @ {address}";
            if (hasLiquidTemperature) ret += $", Liquid @ {liquidTemperature.Value}";
            if (hasPumpSpeed) ret += $", Pump @ {pumpSpeed.Value}";
            if (hasPumpDuty) ret += $"({pumpDuty.Value})";
            if (hasFanSpeed) ret += $", Fan @ {fanSpeed.Value} ({fanControl.Value})";
            if (hasMicroFanSpeed) ret += $", Micro Fan @ {microFanSpeed.Value} ({microFanControl.Value})";

            for (int i = 0; i<20; i++) {
                if (hasMultipleFanSpeed[i]) {
                    ret += $", Fan{i+1} @ {fanSpeedMultiple[i].Value} ({fanControlMultiple[i].Value})";
                }
            }

            return ret;
        }
    }
}
