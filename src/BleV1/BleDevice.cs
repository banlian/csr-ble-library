using System;
using System.Text;

namespace CsrBleLibrary.BleV1
{
    public class BleDevice
    {
        //private static int index;

        public string Address;

        public string Name;

        //public string Service;

        //public string Manu;


        internal uint ConnHandle;

        internal uint ReadHandle;

        internal uint WriteHandle;

        public BleDevice()
        {
            //Service = index++.ToString();
        }

        public BleDevice(string dev)
        {
            string[] devStrings = dev.Split('|');

            if (devStrings.Length < 2)
            {
                throw new FormatException("Device String Format Error");
            }

            Name = devStrings[0].Remove(0, devStrings[0].IndexOf(":", StringComparison.Ordinal)+1);
            Address = devStrings[1].Remove(0, devStrings[1].IndexOf(":", StringComparison.Ordinal) + 1);
            //Service = devStrings[2].Remove(0, 8);
            //Manu = devStrings[3].Remove(0, 5);
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(string.Format("设备:{0}|", string.IsNullOrEmpty(Name) ? "[NULL]" : Name));
            sb.Append(string.Format("地址:{0}|", Address));
            //sb.Append(string.Format("Service:{0}|", Service));
            //sb.Append(string.Format("Manu:{0}", Manu));

            return sb.ToString();
        }
    }
}
