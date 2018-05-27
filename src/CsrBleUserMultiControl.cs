using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsrBleLibrary.BleV2;

namespace CsrBleLibrary
{
    public partial class CsrBleUserMultiControl : UserControl
    {
        private string _selectSearchDeviceString;
        private string _selectSearchAddress;

        private string _selectConnectDeviceString;


        public CsrBleUserMultiControl()
        {
            InitializeComponent();
        }

        private void CsrBleUserMultiControl_Load(object sender, EventArgs e)
        {
            _minRssi = Properties.Settings.Default.MultiMinRssi;

            comboBoxRssiDbm.Items.AddRange(Enumerable.Range(1, 9).Select(r => (object)(r * -10).ToString()).ToArray());
            comboBoxRssiDbm.SelectedItem = _minRssi.ToString();

            CsrBleControl.Only().DeviceSearchEvent += OnDeviceSearchResults;
            CsrBleControl.Only().DeviceConnectEvent += OnDeviceConnectUpdate;

            buttonEnum.PerformClick();
        }

        private void buttonEnum_Click(object sender, EventArgs e)
        {
            buttonEnum.Text = "查找中...";
            buttonEnum.BackColor = Color.Gold;

            CsrBleControl.Only().BleHostScanStart();

            OnDeviceSearchResults(CsrBleControl.Only().GattConnectDevices);
        }

        private async void buttonConnect_Click(object sender, EventArgs e)
        {
            if (listBoxDeviceSearch.SelectedIndex < 0 || string.IsNullOrEmpty(_selectSearchDeviceString))
            {
                MessageBox.Show("未选中设备!");
                return;
            }

            var dev = BleGattDevice.String2Device(_selectSearchDeviceString);
            if (dev != null)
            {
                if (await CsrBleControl.Only().BleHostConnect(dev))
                {
                    OnDeviceConnectResults(CsrBleControl.Only().GattConnectDevices);
                    OnDeviceSearchResults(CsrBleControl.Only().GattSearchDevices);

                    _selectConnectDeviceString = string.Empty;
                    listBoxDeviceConnect.SelectedItem = null;
                }
                else
                {
                    MessageBox.Show("设备连接异常!");
                }
            }
            else
            {
                MessageBox.Show("设备连接异常!");
                OnDeviceConnectResults(CsrBleControl.Only().GattConnectDevices);
            }


            _selectSearchDeviceString = string.Empty;
            _selectSearchAddress = string.Empty;
        }

        private async void buttonDisconnect_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_selectConnectDeviceString))
            {
                await CsrBleControl.Only().BleHostDisconnect(BleGattDevice.GetAddress(_selectConnectDeviceString));
                OnDeviceConnectResults(CsrBleControl.Only().GattConnectDevices);

                _selectConnectDeviceString = string.Empty;
                listBoxDeviceConnect.SelectedItem = null;
            }
            else
            {
                await CsrBleControl.Only().BleHostDisconnect();
                OnDeviceConnectResults(null);
            }
        }

        private void listBoxDeviceSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectSearchDeviceString = listBoxDeviceSearch.SelectedItem.ToString();
            _selectSearchAddress = BleGattDevice.GetAddress(_selectSearchDeviceString);

            CsrBleControl.Only().BleHostScanStop();

        }

        private void listBoxDeviceConnect_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectConnectDeviceString = listBoxDeviceConnect.Text;
        }

        private void OnDeviceSearchResults(List<BleGattDevice> bleGattDevices)
        {
            lock (this)
            {
                if (InvokeRequired)
                {
                    Invoke(new Action<List<BleGattDevice>>(OnDeviceSearchResults), bleGattDevices);
                }
                else
                {
                    listBoxDeviceSearch.Items.Clear();

                    if (bleGattDevices == null)
                    {
                        return;
                    }

                    foreach (BleGattDevice device in bleGattDevices)
                    {
                        if (device.Rssi >= _minRssi)
                        {
                            listBoxDeviceSearch.Items.Add(device.Device2String());
                            if (device.Address == _selectSearchAddress)
                            {
                                listBoxDeviceSearch.SelectedItem = device.Device2String();
                            }
                        }
                    }
                }
            }
        }

        public void OnDeviceConnectResults(List<BleGattDevice> bleGattDevices)
        {
            {
                if (InvokeRequired)
                {
                    Invoke(new Action<List<BleGattDevice>>(OnDeviceConnectResults), bleGattDevices);
                }
                else
                {
                    listBoxDeviceConnect.Items.Clear();

                    if (bleGattDevices == null)
                    {
                        return;
                    }

                    foreach (BleGattDevice device in bleGattDevices)
                    {
                        listBoxDeviceConnect.Items.Add(device.Device2String());
                    }
                }
            }

        }

        private void OnDeviceConnectUpdate(string obj)
        {
            OnDeviceConnectResults(CsrBleControl.Only().GattConnectDevices);
        }

        private void buttonUserStart_Click(object sender, EventArgs e)
        {
            UserStartEvent?.Invoke();
        }

        public event Action UserStartEvent;

        private int _minRssi = -50;

        private void comboBoxRssiDbm_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _minRssi = int.Parse(comboBoxRssiDbm.Text);

                Properties.Settings.Default.MultiMinRssi = _minRssi;
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            OnDeviceSearchResults(CsrBleControl.Only().GattSearchDevices);
        }




        public bool SetConnectDevice(string macString)
        {
            lock (this)
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
}
