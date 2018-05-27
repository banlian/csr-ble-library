using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsrBleLibrary.BleV2;
using CsrBleLibrary.uEnergyHost;
using Win32;

namespace CsrBleLibrary
{
    public class CsrBleControl : LogEventObject
    {
        #region static init

        public static void CsrBleInitialize()
        {
            //csr uenergy host init
            CsrBleControl.Only().BleHostInitialize();

            var i = 0;
            while (i++ < 60)
            {
                Application.DoEvents();
                Thread.Sleep(100);
                if (CsrBleControl.Only().IsBleHosted)
                {
                    break;
                }
            }

            if (!CsrBleControl.Only().IsBleHosted)
            {
                MessageBox.Show("蓝牙打开失败!");
                Application.Exit();
            }
        }

        public static async void CsrBleTerminate()
        {
            if (!CsrBleControl.Only().IsBleHosted)
            {
                return;
            }

            await CsrBleControl.Only().BleHostDisconnect();
            Thread.Sleep(300);
            CsrBleControl.Only().BleHostScanStop();
            Thread.Sleep(300);
            CsrBleControl.Only().BleHostTerminate();
        }



        #endregion


        #region ble host

        private bool _isRun;
        public volatile bool IsBleHosted;

        public void BleHostInitialize()
        {
            Task.Run(() =>
            {
                var transport = new CSR_BLE_TRANSPORT
                {
                    transportType = (byte)CsrBleDefine.CSR_BLE_TRANSPORT_USB,
                    usbDeviceNumber = (byte)CsrBleDefine.CSR_BLE_DEFAULT_USB_DEVICE_NUMBER
                };

                if (CsrBleDll.CsrBleHostInit(false, null, ref transport) == 0)
                {
                    Error("[CSR]:CsrBleHostInit Fail!");
                    throw new InvalidOperationException("BleHostInit CsrBleHostInit Fail!");
                }

                if (!CsrBleDll.CsrBleHostStart(0))
                {
                    Error("[CSR]:CsrBleHostStart Fail!");
                    throw new InvalidOperationException("BleHostInit CsrBleHostStart Fail!");
                }

                Info("[CSR]:BleHostInitialize Success!");
                BleHostProcessMsg();
            });
        }

        public void BleHostTerminate()
        {
            _isRun = false;

            CsrBleDll.CsrBleHostCancel();

            if (!CsrBleDll.CsrBleHostDeinit())
            {
                Error("[CSR]:BleHostTerminate Fail!");
            }
            else
            {
                Info("[CSR]:BleHostTerminate Success!");
            }
        }

        private void BleHostProcessMsg()
        {
            if (_isRun)
            {
                Warning("[CSR]:BleHostProcessMsg is Running...");
                return;
            }

            Debug("[CSR]:BleHostProcessMsg Start...");
            _isRun = true;
            var m = new MSG();
            var handle = new IntPtr(-1);
            int ret;

            while ((ret = User32.GetMessage(ref m, handle, 0, 0)) != 0 && _isRun)
            {
                if (ret == -1)
                {
                    //-1 indicates an error
                }
                else
                {
                    Trace("[CSRMSG]:MSG.message - " + m.message.ToString("X4"));
                    //if (m.message == 50159)
                    {
                        switch ((uint)m.wParam)
                        {
                            //init
                            case CsrBleDefine.CSR_BLE_HOST_READY:
                                if (!OnHostReady(m.lParam > 0))
                                {
                                    Debug("[CSRMSG]:CSR_BLE_HOST_READY ERROR!");
                                    return;
                                }
                                Debug("[CSRMSG]:CSR_BLE_HOST_READY");
                                break;

                            //search result
                            case CsrBleDefine.CSR_BLE_HOST_SEARCH_RESULT:
                                var sr =
                                    (CSR_BLE_DEVICE_SEARCH_RESULT)
                                        Marshal.PtrToStructure(new IntPtr(m.lParam),
                                            typeof(CSR_BLE_DEVICE_SEARCH_RESULT));
                                OnHostSearchResult(sr);
                                Trace("[CSRMSG]:CSR_BLE_HOST_SEARCH_RESULT");
                                break;

                            //search stopped
                            case CsrBleDefine.CSR_BLE_HOST_SEARCH_STOPPED:
                                Debug("[CSRMSG]:CSR_BLE_HOST_SEARCH_STOPPED");
                                break;


                            //connect result
                            case CsrBleDefine.CSR_BLE_HOST_CONNECT_RESULT:
                                var cr =
                                    (CSR_BLE_CONNECT_RESULT)
                                        Marshal.PtrToStructure(new IntPtr(m.lParam), typeof(CSR_BLE_CONNECT_RESULT));
                                OnHostConnectResult(cr);
                                Debug("[CSRMSG]:CSR_BLE_HOST_CONNECT_RESULT");
                                break;

                            //disconnect
                            case CsrBleDefine.CSR_BLE_HOST_DISCONNECTED:
                                var dr =
                                    (CSR_BLE_DISCONNECTED)
                                        Marshal.PtrToStructure(new IntPtr(m.lParam), typeof(CSR_BLE_DISCONNECTED));
                                OnHostDisconnectResult(dr);
                                Trace("[CSRMSG]:CSR_BLE_HOST_DISCONNECTED");
                                break;


                            //discover database
                            case CsrBleDefine.CSR_BLE_CLIENT_DATABASE_DISCOVERY_RESULT:
                                var dbr = (CSR_BLE_DATABASE_DISCOVERY_RESULT)
                                    Marshal.PtrToStructure(new IntPtr(m.lParam),
                                        typeof(CSR_BLE_DATABASE_DISCOVERY_RESULT));
                                OnClientDatabaseDiscoveryResult(dbr);
                                Debug("[CSRMSG]:CSR_BLE_CLIENT_DATABASE_DISCOVERY_RESULT");
                                break;


                            //connection update request
                            case CsrBleDefine.CSR_BLE_HOST_CONNECTION_UPDATE_REQUEST:
                                var cur =
                                    (CSR_BLE_CONNECTION_UPDATE_REQUEST)
                                        Marshal.PtrToStructure(new IntPtr(m.lParam),
                                            typeof(CSR_BLE_CONNECTION_UPDATE_REQUEST));
                                CsrBleDll.CsrBleHostAcceptConnUpdate(cur.connectHandle,
                                    cur.id, true);
                                Debug("[CSRMSG]:CSR_BLE_HOST_CONNECTION_UPDATE_REQUEST");
                                break;

                            //connection update
                            case CsrBleDefine.CSR_BLE_HOST_CONNECTION_UPDATED:
                                var cu =
                                    (CSR_BLE_CONNECTION_UPDATED)
                                        Marshal.PtrToStructure(new IntPtr(m.lParam),
                                            typeof(CSR_BLE_CONNECTION_UPDATED));
                                OnHostConnectionUpdated(cu);
                                Debug("[CSRMSG]:CSR_BLE_HOST_CONNECTION_UPDATED");
                                break;


                            //host just works
                            case CsrBleDefine.CSR_BLE_HOST_JUSTWORKS_REQUEST:
                                var jwr =
                                    (CSR_BLE_JUSTWORKS_REQUEST)
                                        Marshal.PtrToStructure(new IntPtr(m.lParam), typeof(CSR_BLE_JUSTWORKS_REQUEST));
                                CsrBleDll.CsrBleHostJustWorksResult(jwr.deviceAddress, true, true);
                                Debug("[CSRMSG]:CSR_BLE_HOST_JUSTWORKS_REQUEST");
                                break;


                            //scanning
                            case CsrBleDefine.CSR_BLE_HOST_LE_SCAN_STATUS:
                                Debug("[CSRMSG]:CSR_BLE_HOST_LE_SCAN_STATUS");
                                break;

                            //read result
                            case CsrBleDefine.CSR_BLE_CLIENT_CHAR_READ_RESULT:
                                var read = (CSR_BLE_CHAR_READ_RESULT)
                                    Marshal.PtrToStructure(new IntPtr(m.lParam), typeof(CSR_BLE_CHAR_READ_RESULT));
                                OnClientCharReadResult(read);
                                Debug("[CSRMSG]:CSR_BLE_CLIENT_CHAR_READ_RESULT");
                                break;
                            //write result
                            case CsrBleDefine.CSR_BLE_CLIENT_CHAR_WRITE_RESULT:
                                var write = (CSR_BLE_WRITE_RESULT)
                                    Marshal.PtrToStructure(new IntPtr(m.lParam), typeof(CSR_BLE_WRITE_RESULT));
                                OnClientCharWriteResult(write);
                                Debug("[CSRMSG]:CSR_BLE_CLIENT_CHAR_WRITE_RESULT");
                                break;

                            //char notify
                            case CsrBleDefine.CSR_BLE_CLIENT_CHAR_NOTIFICATION:
                                var notify =
                                    (CSR_BLE_CHAR_NOTIFICATION)
                                        Marshal.PtrToStructure(new IntPtr(m.lParam), typeof(CSR_BLE_CHAR_NOTIFICATION));
                                OnClientCharNofity(notify);
                                Trace("[CSRMSG]:CSR_BLE_CLIENT_CHAR_NOTIFICATION");
                                break;


                            case CsrBleDefine.CSR_BLE_HOST_SECURITY_RESULT:
                                //bool encrypting = false;
                                //// security result.

                                //var secRes =
                                //    (CSR_BLE_SECURITY_RESULT)
                                //        Marshal.PtrToStructure(new IntPtr(m.lParam), typeof(CSR_BLE_SECURITY_RESULT));

                                //if (secRes.result != CsrBleDefine.CSR_BLE_SECURITY_BONDING_ESTABLISHED)
                                //{
                                //    CsrBleDll.CsrBleHostCancel();
                                //    CsrBleDll.CsrBleHostDisconnect(CurDevInfo.ConnHandle);
                                //}
                                //else
                                //{
                                //    encrypting = true;
                                //}
                                Trace("[CSRMSG]:CSR_BLE_HOST_SECURITY_RESULT");
                                break;

                            default:
                                Error($"[CSRMSG]:Unhandled uEnergyHost MSG {(uint)m.wParam:X}");
                                break;
                        }
                    }
                }
            }

            Debug("[CSR]:BleHostThread Finish.");
        }

        #endregion

        #region ble message

        private bool OnHostReady(bool status)
        {
            if (status)
            {
                IsBleHosted = true;
                BleHostScanStart();
                Info("[CSR]:uEnergyHost initialized!");
                return true;
            }
            else
            {
                IsBleHosted = false;
                //exit thread
                _isRun = false;
                Error("[CSR]:uEnergyHost initialize fail!");
                return false;
            }

        }

        private void OnHostSearchResult(CSR_BLE_DEVICE_SEARCH_RESULT sr)
        {
            Predicate<CSR_BLE_DEVICE_SEARCH_RESULT> predicate = r => r.deviceAddress.ToString() == sr.deviceAddress.ToString();
            if (_csrSearchResults.Exists(predicate))
            {
                return;
                //csrSearchResults.RemoveAll(predicate);
                //GattSearchDevices.RemoveAll(r => r.Address == sr.deviceAddress.ToString());
            }

            _csrSearchResults.Add(sr);
            GattSearchDevices.Add(new BleGattDevice
            {
                Name = sr.deviceName,
                Address = sr.deviceAddress.ToString(),
                Rssi = sr.rssi,
                ServiceUuid = sr.serviceUuid,
                NService = sr.nDeviceServices
            });

            Debug("[CSR]:OnDeviceSearchEvent");
            OnDeviceSearchEvent(GattSearchDevices);
        }


        private readonly AutoResetEvent _connectEvent = new AutoResetEvent(false);
        private readonly AutoResetEvent _disconnectEvent = new AutoResetEvent(false);

        private void OnHostConnectResult(CSR_BLE_CONNECT_RESULT conn)
        {
            var gattDevice = GattConnectDevices.Last();
            if (conn.result == 0)
            {
                gattDevice.Handle = conn.connectHandle;
                gattDevice.Connected = true;

                CsrBleDll.CsrBleClientDiscoverDatabase(conn.connectHandle);
                Debug("[CSR]:OnHostConnectResult Success");
                OnDeviceConnectEvent("蓝牙连接 SUCCESS:" + gattDevice.Address);
            }
            else
            {
                gattDevice.Connected = false;
                Debug("[CSR]:OnHostConnectResult Fail");
                OnDeviceConnectEvent("蓝牙连接 FAIL:" + gattDevice.Address);
            }

            _connectEvent.Set();
        }

        private void OnHostConnectionUpdated(CSR_BLE_CONNECTION_UPDATED connUpdated)
        {
        }


        private void OnHostDisconnectResult(CSR_BLE_DISCONNECTED disconn)
        {
            var gattDevice = GattConnectDevices.Find(d => d.Handle == disconn.connectHandle);
            if (gattDevice == null)
            {
                Error("[CSR]:Disconnect Device not found!");
                return;
            }

            Debug($"[CSR]:Disconnect Reason {disconn.reason}.");

            gattDevice.Connected = false;
            if (disconn.reason > 0)
            {

                GattConnectDevices.RemoveAll(d => d.Handle == disconn.connectHandle);
                Debug("[CSR]:OnHostDisconnectResult Success");
                OnDeviceConnectEvent("蓝牙断开 SUCCESS:" + gattDevice.Address);
            }
            else
            {
                Debug("[CSR]:OnHostDisconnectResult Fail");
                OnDeviceConnectEvent("蓝牙断开 FAIL:" + gattDevice.Address);
            }

            _disconnectEvent.Set();
        }

        private readonly AutoResetEvent _databaseEvent = new AutoResetEvent(false);

        private void OnClientDatabaseDiscoveryResult(CSR_BLE_DATABASE_DISCOVERY_RESULT database)
        {
            var gattDevice = GattConnectDevices.Find(d => d.Handle == database.connectHandle);
            if (gattDevice == null || database.result != 0)
            {
                Error("[CSR]:Database Discover Error " + gattDevice?.Address);
                return;
            }

            gattDevice.NService = database.nServices;

            //update GattDevice services and characteristics
            for (var i = 0; i < database.nServices; i++)
            {
                var service =
                    (CSR_BLE_SERVICE)
                        Marshal.PtrToStructure(database.services + i * Marshal.SizeOf(typeof(CSR_BLE_SERVICE)),
                            typeof(CSR_BLE_SERVICE));
                Debug($"[CSR]:Service -Uuid:{service.uuid} -nCharacters:{service.nCharacteristics}");

                var gattService = new BleGattService
                {
                    GattDevice = gattDevice,
                    Uuid = service.uuid.uuid16,
                    StartHandle = service.startHandle,
                    EndHandle = service.endHandle,
                    NCharacters = service.nCharacteristics
                };

                gattDevice.AddGattService(gattService);

                // add gattCharacteristic to gattService
                for (var j = 0; j < service.nCharacteristics; j++)
                {
                    var character = (CSR_BLE_CHARACTERISTIC)
                        Marshal.PtrToStructure(
                            service.characteristics + j * Marshal.SizeOf(typeof(CSR_BLE_CHARACTERISTIC)),
                            typeof(CSR_BLE_CHARACTERISTIC));

                    var gattCharacter = new BleGattCharacteristic
                    {
                        Service = gattService,
                        Uuid = character.uuid.uuid16,
                        Handle = character.handle,
                        DeclarationHandle = character.declHandle,
                        NDescriptors = character.nDescriptors,
                        Properties = character.properties
                    };

                    if (character.nDescriptors > 0)
                    {
                        var descriptor =
                            (CSR_BLE_CHARACTERISTIC_DSC)Marshal.PtrToStructure(character.descriptors,
                                typeof(CSR_BLE_CHARACTERISTIC_DSC));
                        gattCharacter.DescriptorHandle = descriptor.handle;
                    }

                    gattService.AddCharacter(gattCharacter);
                }
            }

            _databaseEvent.Set();
        }

        private readonly AutoResetEvent _readEvent = new AutoResetEvent(false);

        private void OnClientCharReadResult(CSR_BLE_CHAR_READ_RESULT readResult)
        {
            if (readResult.result == 0)
            {
                _readBytes = new byte[readResult.charValueSize];
                Marshal.Copy(readResult.charValue, _readBytes, 0, readResult.charValueSize);

                _readEvent.Set();
                Debug("[CSRMSG]:OnClientCharReadResult Success " + _readBytes.Length);
            }
            else
            {
                Error("[CSRMSG]:OnClientCharReadResult Fail!");
            }
        }

        private readonly AutoResetEvent _writeEvent = new AutoResetEvent(false);

        private void OnClientCharWriteResult(CSR_BLE_WRITE_RESULT writeResult)
        {
            if (writeResult.result == 0)
            {
                _writeEvent.Set();
                Debug("[CSRMSG]:OnClientCharWriteResult Success!");
            }
            else
            {
                Error($"[CSRMSG]:OnCharWriteResult Fail {writeResult.result}");
            }
        }

        private void OnClientCharNofity(CSR_BLE_CHAR_NOTIFICATION charData)
        {
            var data = new byte[charData.charValueSize];
            Marshal.Copy(charData.charValue, data, 0, charData.charValueSize);

            var device = GattConnectDevices.Find(d => d.Handle == charData.connectHandle);
            if (device != null)
            {
                Trace($"[CSRMSG]:OnClientCharNofity Handle:{charData.connectHandle} Character:{charData.charHandle}");
                var charcter = device.FindCharacter(charData.charHandle);
                charcter?.OnNotifyEvent(data);
            }
        }

        #endregion

        #region events

        public event Action<List<BleGattDevice>> DeviceSearchEvent;

        public event Action<string> DeviceConnectEvent;

        protected virtual void OnDeviceSearchEvent(List<BleGattDevice> devs)
        {
            DeviceSearchEvent?.Invoke(devs);
        }

        protected virtual void OnDeviceConnectEvent(string str)
        {
            DeviceConnectEvent?.Invoke(str);
        }

        #endregion

        #region connect

        private readonly List<CSR_BLE_DEVICE_SEARCH_RESULT> _csrSearchResults =
            new List<CSR_BLE_DEVICE_SEARCH_RESULT>();

        public List<BleGattDevice> GattSearchDevices = new List<BleGattDevice>(5);
        public List<BleGattDevice> GattConnectDevices = new List<BleGattDevice>(5);

        public void BleHostScanStart()
        {
            if (!IsBleHosted)
            {
                Error("[CSR]:BleHostInitialize Fail!");
                return;
            }

            _csrSearchResults.Clear();
            GattSearchDevices.Clear();

            CsrBleDll.CsrBleHostCancel();
            CsrBleDll.CsrBleHostStopLESearch();
            Thread.Sleep(300);
            CsrBleDll.CsrBleHostStartLEScan();
            Debug("[CSR]:BleHostScanStart");
        }


        public void BleHostScanStop()
        {
            if (!IsBleHosted)
            {
                Error("[CSR]:BleHostInitialize Fail!");
                return;
            }

            CsrBleDll.CsrBleHostStopLESearch();
            Debug("[CSR]:BleHostScanStop");
        }

        public async Task<bool> BleHostConnect(string devAddress)
        {
            var dev = GattSearchDevices.Find(d => d.Address == devAddress);
            if (dev == null)
            {
                return false;
            }

            return await BleHostConnect(dev);
        }

        public async Task<bool> BleHostConnect(BleGattDevice gattDevice)
        {
            if (!IsBleHosted)
            {
                Error("[CSR]:BleHostInitialize Fail!");
                return false;
            }

            CsrBleDll.CsrBleHostStopLESearch();
            if (GattConnectDevices.Exists(d => d.Address == gattDevice.Address))
            {
                Warning($"[CSR]:BleHostConnect GattDevice {gattDevice.Address} Already Connected!");
                return true;
            }

            return await Task.Run(() =>
            {
                var csrDevice = _csrSearchResults.FindAll(r => r.deviceAddress.ToString() == gattDevice.Address);
                if (csrDevice.Count == 0)
                {
                    Error($"[CSR]:BleHostConnect Find GattDevice Error at {gattDevice.Address}");
                    return false;
                }

                GattSearchDevices.RemoveAll(d => d.Address == gattDevice.Address);

                GattConnectDevices.RemoveAll(d => d.Address == gattDevice.Address);
                GattConnectDevices.Add(gattDevice);
                Debug("[CSR]:GattConnectDevices Add Connecting Device!");

                if (CsrBleDll.CsrBleHostSetConnectionParams((ushort)CsrBleDefine.CSR_BLE_PARAMETER_IGNORE,
                    (ushort)CsrBleDefine.CSR_BLE_PARAMETER_IGNORE,
                    CsrBleDefine.MIN_CONN_INTERVAL, CsrBleDefine.MAX_CONN_INTERVAL,
                    CsrBleDefine.SLAVE_LATENCY, CsrBleDefine.LINK_TIMEOUT))
                {
                    //set parameters is success
                    if (CsrBleDll.CsrBleHostConnect(csrDevice[0].deviceAddress))
                    {
                        Debug("[CSR]:CsrBleHostConnect Start!");
                        if (_connectEvent.WaitOne(5000))
                        {
                            Debug($"[CSR]:CsrBleHostConnect {csrDevice[0].deviceAddress} Success!");
                            if (_databaseEvent.WaitOne(5000))
                            {
                                gattDevice.OnDeviceConnectEvent(true);
                                Debug($"[CSR]:DatabaseDiscover {csrDevice[0].deviceAddress} Success!");
                                OnDeviceConnectEvent("蓝牙连接成功:" + csrDevice[0].deviceAddress);
                                return true;
                            }
                        }
                    }
                }

                GattConnectDevices.Remove(gattDevice);
                gattDevice.OnDeviceConnectEvent(false);
                Debug("[CSR]:GattConnectDevices Remove Connect Fail Device!");

                Debug($"[CSR]:CsrBleHostConnect {csrDevice[0].deviceAddress} Fail!");
                OnDeviceConnectEvent("蓝牙连接失败:" + csrDevice[0].deviceAddress);
                return false;
            });
        }

        public async Task BleHostDisconnect()
        {
            while (GattConnectDevices.Count > 0)
            {
                await BleHostDisconnect(GattConnectDevices[0]);
            }
        }

        public async Task<bool> BleHostDisconnect(string devAddress)
        {
            var dev = GattConnectDevices.Find(d => d.Address == devAddress);
            if (dev == null)
            {
                return false;
            }
            return await BleHostDisconnect(dev);
        }

        public async Task<bool> BleHostDisconnect(BleGattDevice gattDevice)
        {
            if (!IsBleHosted)
            {
                Error("[CSR]:BleHostInitialize Fail!");
                return false;
            }

            return await Task.Run(() =>
            {
                if (CsrBleDll.CsrBleHostDisconnect(gattDevice.Handle))
                {
                    Debug("[CSR]:CsrBleHostDisconnect true!");
                    if (_disconnectEvent.WaitOne(2500))
                    {
                        if (!GattConnectDevices.Contains(gattDevice))
                        {
                            Debug($"[CSR]:CsrBleHostDisconnect {gattDevice.Address} Success!");
                            OnDeviceConnectEvent("蓝牙断开成功:" + gattDevice.Address);
                            gattDevice.OnDeviceDisconnectEvent(true);
                            return true;
                        }
                    }
                }

                if (GattConnectDevices.Contains(gattDevice))
                {
                    GattConnectDevices.Remove(gattDevice);
                }

                gattDevice.OnDeviceDisconnectEvent(false);
                Debug($"[CSR]:CsrBleHostDisconnect {gattDevice.Address} Fail!");
                OnDeviceConnectEvent("蓝牙断开失败:" + gattDevice.Address);
                return false;
            });
        }

        #endregion

        #region read write

        private byte[] _readBytes;

        public async Task<byte[]> BleClientReadChar(BleGattCharacteristic character)
        {
            return await Task.Run(() =>
            {
                if (!character.Service.GattDevice.Connected)
                {
                    Error("[CSR]:BleClientReadChar when device is not connect!");
                    return null;
                }

                if (CsrBleDll.CsrBleClientReadCharByHandle(character.Service.GattDevice.Handle, character.Handle))
                {
                    if (_readEvent.WaitOne(5000))
                    {
                        Debug("[CSR]:BleClientReadChar Success!");
                        return _readBytes;
                    }
                }

                Debug("[CSR]:BleClientReadChar Fail!");
                return null;
            });
        }

        public async Task<bool> BleClientWriteChar(BleGattCharacteristic character, byte[] data)
        {
            return await Task.Run(() =>
            {
                if (!character.Service.GattDevice.Connected)
                {
                    Error("[CSR]:BleClientWriteChar when device is not connect!");
                    return false;
                }

                var value = Marshal.AllocHGlobal(data.Length);
                Marshal.Copy(data, 0, value, data.Length);

                if (CsrBleDll.CsrBleClientWriteCharByHandle(
                    character.Service.GattDevice.Handle, false, character.Handle, (ushort)data.Length, value))
                {
                    if (_writeEvent.WaitOne(5000))
                    {
                        Marshal.FreeHGlobal(value);
                        Debug("[CSR]:BleClientWriteChar Success!");
                        return true;
                    }
                }

                Marshal.FreeHGlobal(value);
                Debug("[CSR]:BleClientWriteChar Fail!");
                return false;
            });
        }

        public void BleClientWriteConfig(BleGattCharacteristic character, byte config)
        {
            if (!character.Service.GattDevice.Connected)
            {
                Error("[CSR]:BleClientWriteConfig when device is not connect!");
                return;
            }

            if (!CsrBleDll.CsrBleClientWriteConfiguration(character.Service.GattDevice.Handle, character.DescriptorHandle,
                character.Handle, config))
            {
                Error("[CSR]:BleClientWriteConfig Fail!");
            }
        }

        #endregion

        #region singleton

        private static CsrBleControl ins;

        private CsrBleControl()
        {
        }


        public static CsrBleControl Only()
        {
            return ins ?? (ins = new CsrBleControl());
        }

        #endregion
    }

}