using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsrBleLibrary.BleV2
{
    public class BleGattService
    {
        public BleGattDevice GattDevice { get; internal set; }
        public ushort Uuid { get; internal set; }
        public ushort StartHandle { get; internal set; }
        public ushort EndHandle { get; internal set; }
        public int NCharacters { get; internal set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("\tservice - {0} >>>\n", Uuid.ToString("X4"));
            sb.AppendFormat("\t\tUuid:{0}\n", Uuid.ToString("X4"));
            sb.AppendFormat("\t\tStartHandle:{0}\n", StartHandle.ToString("X4"));
            sb.AppendFormat("\t\tEndHandle:{0}\n\n", EndHandle.ToString("X4"));

            sb.AppendFormat("\t\tcharacteristics - {0} >>>\n\n", NCharacters);
            foreach (var character in Characteristics)
            {
                sb.Append(character.Value);
            }
            sb.AppendFormat("\t\tcharacteristics - {0} <<<\n", NCharacters);
            sb.AppendFormat("\tservice - {0} <<<\n\n", Uuid.ToString("X4"));

            return sb.ToString();
        }

        #region characters

        private readonly Dictionary<ushort, BleGattCharacteristic> Characteristics =
            new Dictionary<ushort, BleGattCharacteristic>();

        public void AddCharacter(BleGattCharacteristic characteristic)
        {
            if (Characteristics.ContainsKey(characteristic.Uuid))
            {
                Characteristics.Remove(characteristic.Uuid);
            }
            Characteristics.Add(characteristic.Uuid, characteristic);
        }

        public BleGattCharacteristic GattCharacter(ushort uuid)
        {
            if (Characteristics.ContainsKey(uuid))
            {
                return Characteristics[uuid];
            }
            return null;
        }

        public BleGattCharacteristic FindCharacter(ushort charHandle)
        {
            return (from c in Characteristics where c.Value.Handle == charHandle select c.Value).FirstOrDefault();
        }

        #endregion
    }
}