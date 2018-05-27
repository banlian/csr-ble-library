using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsrBleLibrary.BleV2
{
    public class BleGattDevice
    {
        public BleGattDevice()
        {
            Status = true;
        }

        /// <summary>
        /// indicate device calib process status
        /// </summary>
        public bool Status { get; set; }

        public uint Handle { get; set; }
        public sbyte Rssi { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool Connected { get; set; }
        
        public ushort ServiceUuid { get; set; }
        public ushort NService { get; set; }

        public event Action<bool> DeviceConnectEvent;
        public event Action<bool> DeviceDisconnectEvent;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("device - {0} >>>\n", Address);
            sb.AppendFormat("\tHandle:{0:X}\n", Handle);
            sb.AppendFormat("\tRssi:{0}\n", Rssi);
            sb.AppendFormat("\tName:{0}\n", Name);
            sb.AppendFormat("\tAddress:{0}\n", Address);
            sb.AppendFormat("\tConnected:{0}\n", Connected);
            sb.AppendFormat("\tServiceUuid:{0:X}\n", ServiceUuid);
            sb.AppendFormat("\tNService:{0}\n", NService);
            sb.AppendFormat("device - {0} <<<\n\n", Address);

            sb.AppendFormat("device {0} - service >>>\n", Address);
            foreach (var gattService in Services)
            {
                sb.Append(gattService.Value);
            }
            sb.AppendFormat("device {0} - service <<<\n\n", Address);
            return sb.ToString();
        }

        public virtual void OnDeviceConnectEvent(bool status)
        {
            DeviceConnectEvent?.Invoke(status);
        }

        public virtual void OnDeviceDisconnectEvent(bool status)
        {
            DeviceDisconnectEvent?.Invoke(status);
        }

        #region services

        private readonly Dictionary<ushort, BleGattService> Services = new Dictionary<ushort, BleGattService>();

        public void AddGattService(BleGattService service)
        {
            if (Services.ContainsKey(service.Uuid))
            {
                Services.Remove(service.Uuid);
            }
            Services.Add(service.Uuid, service);
        }

        public BleGattService GattService(ushort uuid)
        {
            if (Services.ContainsKey(uuid))
            {
                return Services[uuid];
            }
            return null;
        }

        #endregion

        #region characters

        public BleGattCharacteristic GattCharacter(ushort uuid)
        {
            return Services.Select(s => s.Value.GattCharacter(uuid)).FirstOrDefault(c => c != null);
        }

        public BleGattCharacteristic FindCharacter(ushort charHandle)
        {
            return Services.Select(s => s.Value.FindCharacter(charHandle)).FirstOrDefault(c => c != null);
        }

        #endregion

        #region string/device convert methods

        public string Device2String()
        {
            return $"设备:{Name}|地址:{Address}|信号:{Rssi}dBm";
        }

        public static BleGattDevice String2Device(string str)
        {
            var data = str.Split('|');

            if (data.Length < 2)
            {
                return null;
            }


            var device = new BleGattDevice
            {
                Name = data[0].Substring(data[0].IndexOf(":", StringComparison.Ordinal) + 1),
                Address = data[1].Substring(data[1].IndexOf(":", StringComparison.Ordinal) + 1)
            };

            return device;
        }



        public static string GetAddress(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            var data = str.Split('|');

            if (data.Length < 2)
            {
                return null;
            }

            return data[1].Substring(data[1].IndexOf(":", StringComparison.Ordinal) + 1);
        }



        #endregion
    }
}