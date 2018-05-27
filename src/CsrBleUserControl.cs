using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsrBleLibrary.BleV2;

namespace CsrBleLibrary
{
    public partial class CsrBleUserControl : UserControl
    {
        public CsrBleUserControl()
        {
            InitializeComponent();
        }

        private void CsrBleUserControl_Load(object sender, EventArgs e)
        {
            CsrBleControl.Only().DeviceSearchEvent += OnUpdateDeviceSearchResults;

            buttonEnum.PerformClick();
        }

        public void SetSplitterDistance(float dis)
        {
            if (dis > 0 && dis < 1)
            {
                splitContainerMain.SplitterDistance = (int)(splitContainerMain.Width * dis);
            }
        }

        public void SetMinDbm(int dbm)
        {
            MinDbm = dbm;
        }

        public int MinDbm { get; set; } = -50;

        #region enum

        private void buttonEnum_Click(object sender, EventArgs e)
        {
            labelBleConnectStatus.Text = "查找中...";
            labelBleConnectStatus.BackColor = Color.Gold;

            CsrBleControl.Only().BleHostScanStart();

            OnUpdateDeviceSearchResults(CsrBleControl.Only().GattSearchDevices);
        }

        private void OnUpdateDeviceSearchResults(List<BleGattDevice> bleGattDevices)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<List<BleGattDevice>>(OnUpdateDeviceSearchResults), bleGattDevices);
            }
            else
            {
                listBoxDeviceSearch.Items.Clear();

                foreach (BleGattDevice device in bleGattDevices)
                {
                    if (device.Rssi > MinDbm)
                    {
                        listBoxDeviceSearch.Items.Add(device.Device2String());
                        if (device.Address == SelectAddr)
                        {
                            listBoxDeviceSearch.SelectedItem = device.Device2String();
                        }
                    }
                }
            }
        }

        public void OnUpdateDeviceConnectResult(BleGattDevice dev)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<BleGattDevice>(OnUpdateDeviceConnectResult), dev);
            }
            else
            {
                if (dev != null)
                {
                    labelBleConnectStatus.Text = dev.Device2String();
                    labelBleConnectStatus.BackColor = Color.Lime;
                }
                else
                {
                    labelBleConnectStatus.Text = string.Empty;
                    labelBleConnectStatus.BackColor = Color.LightGray;
                }
            }
        }

        private void listBoxDeviceSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectDeviceString = listBoxDeviceSearch.Text?.ToString();
            SelectAddr = BleGattDevice.GetAddress(SelectDeviceString);

            CsrBleControl.Only().BleHostScanStop();
        }

        private string SelectDeviceString;
        private string SelectAddr;


        #endregion

        #region connect

        private async void buttonConnect_Click(object sender, EventArgs e)
        {
            if (listBoxDeviceSearch.SelectedIndex < 0 || string.IsNullOrEmpty(SelectDeviceString))
            {
                MessageBox.Show("未选择设备！");
                return;
            }


            var dev = BleGattDevice.String2Device(SelectDeviceString);
            if (dev != null)
            {
                if (await CsrBleControl.Only().BleHostConnect(dev))
                {
                    OnDeviceConnectEvent(dev);
                    OnUpdateDeviceConnectResult(dev);
                }
            }
            else
            {
                OnDeviceConnectEvent(null);
                OnUpdateDeviceConnectResult(null);
            }

            listBoxDeviceSearch.SelectedItem = null;
            SelectDeviceString = string.Empty;
            SelectAddr = string.Empty;
        }

        private async void buttonDisconnect_Click(object sender, EventArgs e)
        {
            OnDeviceDisconnectEvent(null);

            await CsrBleControl.Only().BleHostDisconnect();

            OnUpdateDeviceConnectResult(null);

            listBoxDeviceSearch.SelectedItem = null;
            SelectDeviceString = string.Empty;
            SelectAddr = string.Empty;
        }

        public event Action<BleGattDevice> DeviceConnectEvent;

        protected virtual void OnDeviceConnectEvent(BleGattDevice dev)
        {
            DeviceConnectEvent?.Invoke(dev);
        }

        public event Action<BleGattDevice> DeviceDisconnectEvent;

        protected virtual void OnDeviceDisconnectEvent(BleGattDevice dev)
        {
            DeviceDisconnectEvent?.Invoke(dev);
        }

        #endregion



        public bool SetConnectDevice(string macString)
        {
            foreach (var item in listBoxDeviceSearch.Items)
            {
                if (item.ToString().Contains(macString))
                {
                    listBoxDeviceSearch.SelectedItem = item;
                    return true;
                }
            }
            return false;
        }
    }
}