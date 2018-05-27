using System;
using System.Text;
using System.Threading.Tasks;

namespace CsrBleLibrary.BleV2
{
    public class BleGattCharacteristic
    {
        public BleGattService Service { get; internal set; }
        public ushort Uuid { get; set; }
        public ushort Handle { get; internal set; }
        public ushort DeclarationHandle { get; internal set; }
        public int NDescriptors { get; internal set; }
        public ushort DescriptorHandle { get; set; }
        public byte Properties { get; set; }

        public event Action<byte[]> NotifyEvent;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("\t\t\tUuid:{0:X4}\n", Uuid);
            sb.AppendFormat("\t\t\tHandle:{0:X4}\n", Handle);
            sb.AppendFormat("\t\t\tDeclarationHandle:{0:X4}\n", DeclarationHandle);
            sb.AppendFormat("\t\t\tNDescriptors:{0:X}\n", NDescriptors);
            sb.AppendFormat("\t\t\tDescriptorHandle:{0:X4}\n", DescriptorHandle);
            sb.AppendFormat("\t\t\tProperties:{0:X}\n\n", Properties);
            return sb.ToString();
        }

        public virtual void OnNotifyEvent(byte[] obj)
        {
            NotifyEvent?.Invoke(obj);
        }

        #region character methods

        public async Task<bool> WriteValue(byte[] data)
        {
            return await CsrBleControl.Only().BleClientWriteChar(this, data);
        }

        public async Task<byte[]> ReadValue()
        {
            return await CsrBleControl.Only().BleClientReadChar(this);
        }

        public void Config(bool enable)
        {
            if (enable)
            {
                CsrBleControl.Only().BleClientWriteConfig(this, (byte) GattClientCharacterConfigDescValue.Notify);
            }
            else
            {
                CsrBleControl.Only().BleClientWriteConfig(this, (byte) GattClientCharacterConfigDescValue.None);
            }
        }

        #endregion
    }

    [Flags]
    public enum GattCharacteristicProperties
    {
        None = 0,
        Broadcast = 1,
        Read = 2,
        WriteWithoutResponse = 4,
        Write = 8,
        Notify = 16,
        Indicate = 32,
        AuthenticatedSignedWrites = 64,
        ExtendedProperties = 128,
        ReliableWrites = 256,
        WritableAuxiliaries = 512
    }

    public enum GattClientCharacterConfigDescValue
    {
        None = 0x00,
        Notify = 0x01,
        Indicate = 0x02
    }
}