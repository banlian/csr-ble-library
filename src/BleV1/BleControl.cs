using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BaseLibrary;
using BaseLibrary.Object;
using Win32;

namespace CsrBleLibrary.BleV1
{
    public class BleControlV1 : LogEventObject
    {
        private static readonly List<BleDevice> DevInfos = new List<BleDevice>();


        public BleDevice DevInfoSelected { get; private set; }
        public BleDevice CurDevInfo { get; private set; }

        #region static methods

        public int GetServicePtrPos(ushort uuid16, ref CSR_BLE_DATABASE_DISCOVERY_RESULT dbRes)
        {
            int serviceCount = dbRes.nServices;
            CSR_BLE_SERVICE[] services = new CSR_BLE_SERVICE[serviceCount];
            for (int j = 0; j < serviceCount; j++)
            {
                services[j] =
                    (CSR_BLE_SERVICE)
                        Marshal.PtrToStructure(dbRes.services + j*Marshal.SizeOf(typeof (CSR_BLE_SERVICE)),
                            typeof (CSR_BLE_SERVICE));
                Debug(string.Format("Service -Uuid:{0} -nCharacters:{1}", services[j].uuid, services[j].nCharacteristics));
            }

            int serviceId = 0;
            while (serviceId < serviceCount)
            {
                if (services[serviceId].uuid.uuid16 == uuid16)
                    return serviceId;
                serviceId++;
            }
            return 0xFF;
        }

        #endregion

        #region ble host

        private bool isRun;
        private bool isBleInitialized;

        public void BleHostInitialize()
        {
            Task.Run(() =>
            {
                CSR_BLE_TRANSPORT transport = new CSR_BLE_TRANSPORT()
                {
                    transportType = (byte) CsrBleDefine.CSR_BLE_TRANSPORT_USB,
                    usbDeviceNumber = (byte) CsrBleDefine.CSR_BLE_DEFAULT_USB_DEVICE_NUMBER,
                };

                if (CsrBleDll.CsrBleHostInit(false, null, ref transport) == 0)
                {
                    Error("Unable to initialize uEnergy Host.");
                    MessageBox.Show("蓝牙无法初始化！");
                    return;
                }

                if (!CsrBleDll.CsrBleHostStart(0))
                {
                    Error("Unable to start uEnergy Host.");
                    MessageBox.Show("蓝牙HOST无法开始！");
                    return;
                }

                Trace("BleHostInitialize");
                BleHostProcessMsg();
            });
        }

        public void BleHostTerminate()
        {
            isRun = false;

            if (!CsrBleDll.CsrBleHostDeinit())
            {
                Error("Unable to de-initialize uEnergy Host.");
            }
            else
            {
                OnLogEvent("BleHostTerminate");
            }
        }

        public void BleHostScanStart()
        {
            if (!isBleInitialized)
            {
                Error("BleHostInitialize Fail!");
                return;
            }
            bleSearchResults.Clear();
            DevInfos.Clear();

            CsrBleDll.CsrBleHostCancel();
            CsrBleDll.CsrBleHostStopLESearch();
            CsrBleDll.CsrBleHostStartLEScan();
        }


        public void BleHostScanStop()
        {
            if (!isBleInitialized)
            {
                Error("BleHostInitialize Fail!");
                return;
            }
            CsrBleDll.CsrBleHostStopLESearch();
        }

        #endregion

        #region ble message

        public void BleHostProcessMsg()
        {
            if (isRun)
            {
                OnLogEvent("BleHostThread is Running...");
                return;
            }
            OnLogEvent("BleHostThread Start...");

            isRun = true;
            MSG m = new MSG();
            IntPtr handle = new IntPtr(-1);
            int ret;

            while ((ret = User32.GetMessage(ref m, handle, 0, 0)) != 0 && isRun)
            {
                if (ret == -1)
                {
                    //-1 indicates an error
                }
                else
                {
                    Trace("[BleMsg]:MSG.message - " + m.message.ToString("X4"));
                    //if (m.message == 50159)
                    {
                        switch ((uint) m.wParam)
                        {
                            //init
                            case CsrBleDefine.CSR_BLE_HOST_READY:
                                isBleInitialized = m.lParam > 0;
                                if (isBleInitialized)
                                {
                                    Info("[BleMsg]:uEnergy Host library is initialized");
                                    BleHostScanStart();
                                    Info("[BleMsg]:CSR_BLE_HOST_READY");
                                }
                                else
                                {
                                    //exit thread
                                    isRun = false;
                                    Error("Dongle failed to initialize! Please insert the dongle to computer!");
                                    //MessageBox.Show("蓝牙初始化失败！");
                                    return;
                                }
                                break;

                            //search result
                            case CsrBleDefine.CSR_BLE_HOST_SEARCH_RESULT:
                                var sr =
                                    (CSR_BLE_DEVICE_SEARCH_RESULT)
                                        Marshal.PtrToStructure(new IntPtr(m.lParam),
                                            typeof (CSR_BLE_DEVICE_SEARCH_RESULT));
                                OnSearchResult(sr);
                                Trace("[BleMsg]:CSR_BLE_HOST_SEARCH_RESULT");
                                break;

                            //search stopped
                            case CsrBleDefine.CSR_BLE_HOST_SEARCH_STOPPED:
                                Info("[BleMsg]:CSR_BLE_HOST_SEARCH_STOPPED");
                                break;


                            //connect result
                            case CsrBleDefine.CSR_BLE_HOST_CONNECT_RESULT:
                                var cr =
                                    (CSR_BLE_CONNECT_RESULT)
                                        Marshal.PtrToStructure(new IntPtr(m.lParam), typeof (CSR_BLE_CONNECT_RESULT));
                                if (cr.connectHandle != 0)
                                {
                                    OnConnectResult(cr);
                                }
                                Trace("[BleMsg]:CSR_BLE_HOST_CONNECT_RESULT");
                                break;

                            //disconnect
                            case CsrBleDefine.CSR_BLE_HOST_DISCONNECTED:
                                var dr =
                                    (CSR_BLE_DISCONNECTED)
                                        Marshal.PtrToStructure(new IntPtr(m.lParam), typeof (CSR_BLE_DISCONNECTED));
                                if (dr.connectHandle != 0)
                                {
                                    OnDisconnectResult(dr);
                                }
                                Trace("[BleMsg]:CSR_BLE_HOST_DISCONNECTED");
                                break;


                            //discover database
                            case CsrBleDefine.CSR_BLE_CLIENT_DATABASE_DISCOVERY_RESULT:
                                var dbRes = (CSR_BLE_DATABASE_DISCOVERY_RESULT)
                                    Marshal.PtrToStructure(new IntPtr(m.lParam),
                                        typeof (CSR_BLE_DATABASE_DISCOVERY_RESULT));
                                if (dbRes.result == 0)
                                {
                                    OnDatabaseDiscoveryResult(dbRes);
                                }

                                Trace("[BleMsg]:CSR_BLE_CLIENT_DATABASE_DISCOVERY_RESULT");
                                break;


                            //connection update req
                            case CsrBleDefine.CSR_BLE_HOST_CONNECTION_UPDATE_REQUEST:
                                var connUpdateRequest =
                                    (CSR_BLE_CONNECTION_UPDATE_REQUEST)
                                        Marshal.PtrToStructure(new IntPtr(m.lParam),
                                            typeof (CSR_BLE_CONNECTION_UPDATE_REQUEST));
                                CsrBleDll.CsrBleHostAcceptConnUpdate(connUpdateRequest.connectHandle,
                                    connUpdateRequest.id, true);
                                Trace("[BleMsg]:CSR_BLE_HOST_CONNECTION_UPDATE_REQUEST");

                                break;

                            //connection update
                            case CsrBleDefine.CSR_BLE_HOST_CONNECTION_UPDATED:
                                var connUpdated =
                                    (CSR_BLE_CONNECTION_UPDATED)
                                        Marshal.PtrToStructure(new IntPtr(m.lParam),
                                            typeof (CSR_BLE_CONNECTION_UPDATED));
                                OnConnectionUpdated(connUpdated);
                                Trace("[BleMsg]:CSR_BLE_HOST_CONNECTION_UPDATED");
                                break;


                            //host just works
                            case CsrBleDefine.CSR_BLE_HOST_JUSTWORKS_REQUEST:
                                var justworksRequest =
                                    (CSR_BLE_JUSTWORKS_REQUEST)
                                        Marshal.PtrToStructure(new IntPtr(m.lParam), typeof (CSR_BLE_JUSTWORKS_REQUEST));
                                CsrBleDll.CsrBleHostJustWorksResult(justworksRequest.deviceAddress, true, true);
                                Trace("[BleMsg]:CSR_BLE_HOST_JUSTWORKS_REQUEST");
                                break;


                            //scanning
                            case CsrBleDefine.CSR_BLE_HOST_LE_SCAN_STATUS:
                                Trace("[BleMsg]:CSR_BLE_HOST_LE_SCAN_STATUS");
                                break;

                            //read result
                            case CsrBleDefine.CSR_BLE_CLIENT_CHAR_READ_RESULT:
                                var readResult = (CSR_BLE_CHAR_READ_RESULT)
                                    Marshal.PtrToStructure(new IntPtr(m.lParam), typeof (CSR_BLE_CHAR_READ_RESULT));
                                OnCharReadResult(readResult);
                                Trace("[BleMsg]:CSR_BLE_CLIENT_CHAR_READ_RESULT");
                                break;
                            //write result
                            case CsrBleDefine.CSR_BLE_CLIENT_CHAR_WRITE_RESULT:
                                var writeResult = (CSR_BLE_WRITE_RESULT)
                                    Marshal.PtrToStructure(new IntPtr(m.lParam), typeof (CSR_BLE_WRITE_RESULT));
                                OnCharWriteResult(writeResult);
                                Trace("[BleMsg]:CSR_BLE_CLIENT_CHAR_WRITE_RESULT");
                                break;

                            //char notify
                            case CsrBleDefine.CSR_BLE_CLIENT_CHAR_NOTIFICATION:
                                var charData =
                                    (CSR_BLE_CHAR_NOTIFICATION)
                                        Marshal.PtrToStructure(new IntPtr(m.lParam), typeof (CSR_BLE_CHAR_NOTIFICATION));
                                OnClientCharNofity(charData);
                                Trace("[BleMsg]:CSR_BLE_CLIENT_CHAR_NOTIFICATION");
                                break;


                            case CsrBleDefine.CSR_BLE_HOST_SECURITY_RESULT:
                                //bool encrypting = false;
                                //// security result.

                                //var secRes =
                                //    (CSR_BLE_SECURITY_RESULT)
                                //        Marshal.PtrToStructure(new IntPtr(m.lParam), typeof(CSR_BLE_SECURITY_RESULT));

                                //if (secRes.result != CsrBleDefines.CSR_BLE_SECURITY_BONDING_ESTABLISHED)
                                //{
                                //    CsrBleDll.CsrBleHostCancel();
                                //    CsrBleDll.CsrBleHostDisconnect(CurDevInfo.ConnHandle);
                                //}
                                //else
                                //{
                                //    encrypting = true;
                                //}
                                Trace("[BleMsg]:CSR_BLE_HOST_SECURITY_RESULT");
                                break;

                            default:
                                Error(string.Format("[BleMsg]:Unhandled uEnergy Host library MSG {0:X}",
                                    (uint) m.wParam));
                                break;
                        }
                    }
                }
            }

            OnLogEvent("BleHostThread Finish.");
        }

        private void OnConnectResult(CSR_BLE_CONNECT_RESULT cr)
        {
            CurDevInfo.ConnHandle = cr.connectHandle;
            CsrBleDll.CsrBleClientDiscoverDatabase(cr.connectHandle);
            IsConnected = true;
            OnUpdateBleConnectEvent("蓝牙连接:" + CurDevInfo);
        }

        private void OnDisconnectResult(CSR_BLE_DISCONNECTED dr)
        {
            IsConnected = false;
            OnUpdateBleConnectEvent("蓝牙断开:" + CurDevInfo);
            CurDevInfo = null;
        }

        private void OnConnectionUpdated(CSR_BLE_CONNECTION_UPDATED connUpdated)
        {
        }

        private void OnCharReadResult(CSR_BLE_CHAR_READ_RESULT readResult)
        {
            if (readResult.result == 0)
            {
                _readBytes = new byte[readResult.charValueSize];
                Marshal.Copy(readResult.charValue, _readBytes, 0, readResult.charValueSize);

                if (_curServiceUuid == 0xf000)
                {
                }
                else
                {
                    _readEvent.Set();
                }

                Debug("[BleMsg]:uEnergy Host read the data length" + _readBytes.Length);
            }
            else
            {
                Error("[BleMsg]:uEnergy Host fails to read the data.\n");
            }
        }

        private void OnCharWriteResult(CSR_BLE_WRITE_RESULT writeResult)
        {
            if (writeResult.result == 0)
            {
                _writeEvent.Set();
                Trace("[BleMsg]:CSR_BLE_CLIENT_CHAR_READ_RESULT Write SUCCESS.");
            }
            else
            {
                Trace("[BleMsg]:CSR_BLE_CLIENT_CHAR_READ_RESULT Write FAIL.");
            }
        }

        public void OnSearchResult(CSR_BLE_DEVICE_SEARCH_RESULT searchResult)
        {
            if (bleSearchResults.Count > 0)
            {
                var all =
                    bleSearchResults.FindAll(r => r.deviceAddress.ToString() == searchResult.deviceAddress.ToString());
                if (all.Count > 0)
                {
                    return;
                }
            }

            bleSearchResults.Add(searchResult);
            DevInfos.Add(new BleDevice()
            {
                Name = searchResult.deviceName,
                Address = searchResult.deviceAddress.ToString(),
                //Service = result.serviceUuid.ToString("X2"),
                //Manu = result.manufCode.ToString("X2"),
            });
            OnEnumDeviceEvent(DevInfos);
        }


        private void OnDatabaseDiscoveryResult(CSR_BLE_DATABASE_DISCOVERY_RESULT database)
        {
            int servicePtrPos = GetServicePtrPos((ushort) _curServiceUuid, ref database);
            if (servicePtrPos != 0xFF)
            {
                CSR_BLE_SERVICE service =
                    (CSR_BLE_SERVICE) Marshal.PtrToStructure(
                        database.services + servicePtrPos*Marshal.SizeOf(typeof (CSR_BLE_SERVICE)),
                        typeof (CSR_BLE_SERVICE));

                //if (_curServiceUuid != XIM_BATTERY_SERVICE)
                {
                    var nCharacter = service.nCharacteristics;
                    CSR_BLE_CHARACTERISTIC readCharcter =
                        (CSR_BLE_CHARACTERISTIC)
                            Marshal.PtrToStructure(service.characteristics, typeof (CSR_BLE_CHARACTERISTIC));

                    CSR_BLE_CHARACTERISTIC writeCharcter =
                        (CSR_BLE_CHARACTERISTIC)
                            Marshal.PtrToStructure(
                                service.characteristics + 1*Marshal.SizeOf(typeof (CSR_BLE_CHARACTERISTIC)),
                                typeof (CSR_BLE_CHARACTERISTIC));

                    CurDevInfo.ReadHandle = readCharcter.handle;
                    CurDevInfo.WriteHandle = writeCharcter.handle;

                    if (readCharcter.nDescriptors > 0)
                    {
                        var descHandle =
                            (CSR_BLE_CHARACTERISTIC_DSC) Marshal.PtrToStructure(readCharcter.descriptors,
                                typeof (CSR_BLE_CHARACTERISTIC_DSC));
                        CsrBleDll.CsrBleClientWriteConfiguration(CurDevInfo.ConnHandle, descHandle.handle,
                            readCharcter.handle, 0x01);
                    }
                }
                _dbDiscoverEvent.Set();
            }
        }

        private void OnClientCharNofity(CSR_BLE_CHAR_NOTIFICATION charData)
        {
            byte[] data = new byte[charData.charValueSize];
            Marshal.Copy(charData.charValue, data, 0, charData.charValueSize);

            if (_curServiceUuid == 0xf000)
            {
                OnRawDataEvent(data);
            }
            else if (_curServiceUuid == 0xf100)
            {
                //var rawData = imuCalibParser.ParseData(data);
                //if (rawData != null)
                //{
                //    Trace(rawData.ToString());
                //    OnRawDataEvent(rawData);
                //}
            }
        }

        public event Action<object> RawDataEvent;

        protected virtual void OnRawDataEvent(object obj)
        {
            if (RawDataEvent != null) RawDataEvent(obj);
        }

        #endregion

        #region connect

        public event Action<List<BleDevice>> EnumDeviceEvent;

        private readonly List<CSR_BLE_DEVICE_SEARCH_RESULT> bleSearchResults = new List<CSR_BLE_DEVICE_SEARCH_RESULT>();

        public void ClearDevice()
        {
            bleSearchResults.Clear();
            DevInfos.Clear();

            DevInfoSelected = null;
        }

        protected virtual void OnEnumDeviceEvent(List<BleDevice> obj)
        {
            if (EnumDeviceEvent != null) EnumDeviceEvent(obj);
        }


        public bool IsConnected { get; private set; }

        public void SetCurDevice(string dev)
        {
            DevInfoSelected = new BleDevice(dev);
        }

        public bool IsSelected()
        {
            return DevInfoSelected != null;
        }

        public void ResetConnect()
        {
            BleHostScanStop();

            BleHostDisconnect();

            ClearDevice();
        }

        public bool BleHostConnect()
        {
            CsrBleDll.CsrBleHostStopLESearch();

            if (DevInfoSelected == null)
            {
                Error("BleHostConnect No Device Selected!");
                return false;
            }

            if (CurDevInfo != null)
            {
                BleHostDisconnect();
            }

            CurDevInfo = DevInfoSelected;
            DevInfoSelected = null;

            if (CurDevInfo != null)
            {
                string addr = CurDevInfo.Address;
                var dev = bleSearchResults.FindAll(r => r.deviceAddress.ToString() == addr);
                if (dev.Count == 0)
                {
                    Error(string.Format("BleHostConnect Find Device Error at {0}", addr));
                    return false;
                }


                if (CsrBleDll.CsrBleHostSetConnectionParams((ushort) CsrBleDefine.CSR_BLE_PARAMETER_IGNORE,
                    (ushort) CsrBleDefine.CSR_BLE_PARAMETER_IGNORE,
                    CsrBleDefine.MIN_CONN_INTERVAL,
                    CsrBleDefine.MAX_CONN_INTERVAL,
                    CsrBleDefine.SLAVE_LATENCY,
                    CsrBleDefine.LINK_TIMEOUT))
                {
                    //set parameters is success
                    if (CsrBleDll.CsrBleHostConnect(dev[0].deviceAddress))
                    {
                        if (_dbDiscoverEvent.WaitOne(8000))
                        {
                            Trace(string.Format("CsrBleHostConnect {0} Success!", dev[0].deviceAddress));
                            OnUpdateBleConnectEvent("蓝牙连接成功:" + dev[0].deviceAddress);
                            return true;
                        }
                    }
                }
                Trace(string.Format("CsrBleHostConnect {0} Fail!", dev[0].deviceAddress));
                OnUpdateBleConnectEvent("蓝牙连接失败:" + dev[0].deviceAddress);
            }
            return false;
        }


        public void BleHostDisconnect()
        {
            CsrBleDll.CsrBleHostCancel();
            if (CurDevInfo != null)
            {
                if (CsrBleDll.CsrBleHostDisconnect(CurDevInfo.ConnHandle))
                {
                    Trace(string.Format("CsrBleHostDisconnect {0} Success!", CurDevInfo));
                    OnUpdateBleConnectEvent("蓝牙断开成功:" + CurDevInfo);
                    return;
                }
                Trace(string.Format("CsrBleHostDisconnect {0} Fail!", CurDevInfo));
                OnUpdateBleConnectEvent("蓝牙断开失败:" + CurDevInfo);
            }
        }

        public event Action<string> UpdateBleConnectEvent;

        protected virtual void OnUpdateBleConnectEvent(string obj)
        {
            if (UpdateBleConnectEvent != null) UpdateBleConnectEvent(obj);
        }

        #endregion

        #region mode control

        private uint _curServiceUuid = 0xf000;


        private readonly AutoResetEvent _dbDiscoverEvent = new AutoResetEvent(false);

        public bool BleSwitchMode()
        {
            if (!IsConnected)
            {
                Error("BleWriteChar when no device is connect!");
            }

            //switch (mode)
            //{
            //    case XcobraDataMode.NORMAL:
            //        _curServiceUuid = XIM_NORMAL_SERVICE;
            //        break;
            //    //case XcobraDataMode.BLOBS:
            //    //    _curServiceUuid = XIM_BLOBS_SERVICE;
            //    //    break;
            //    case XcobraDataMode.CALIB:
            //        _curServiceUuid = XIM_IMU_RAW_SERVICE;
            //        break;
            //    case XcobraDataMode.BATTERY:
            //        _curServiceUuid = XIM_BATTERY_SERVICE;
            //        break;

            //    default:
            //        return true;

            //}

            if (CurDevInfo != null)
            {
                CsrBleDll.CsrBleClientDiscoverDatabase(CurDevInfo.ConnHandle);

                if (_dbDiscoverEvent.WaitOne(8000))
                {
                    OnLogEvent("BleSwitchMode Success!");
                    return true;
                }
            }

            OnLogEvent("BleSwitchMode Fail!");
            return false;
        }


        private readonly AutoResetEvent _writeEvent = new AutoResetEvent(false);

        public bool BleWriteChar(byte[] data)
        {
            if (!IsConnected)
            {
                Error("BleWriteChar when no device is connect!");
            }

            IntPtr value = Marshal.AllocHGlobal(data.Length);
            Marshal.Copy(data, 0, value, data.Length);

            if (CsrBleDll.CsrBleClientWriteCharByHandle(CurDevInfo.ConnHandle, false, (ushort) CurDevInfo.WriteHandle,
                (ushort) data.Length, value))
            {
                Marshal.FreeHGlobal(value);

                if (_writeEvent.WaitOne(5000))
                {
                    Debug("BleWriteChar Success!");
                    return true;
                }
            }
            Marshal.FreeHGlobal(value);
            return false;
        }

        private readonly AutoResetEvent _readEvent = new AutoResetEvent(false);

        private byte[] _readBytes;

        public byte[] BleReadChar()
        {
            if (!IsConnected)
            {
                Error("BleWriteChar when no device is connect!");
            }

            if (CsrBleDll.CsrBleClientReadCharByHandle(CurDevInfo.ConnHandle, (ushort) CurDevInfo.ReadHandle))
            {
                if (_readEvent.WaitOne(5000))
                {
                    Debug("BleWriteChar SUCCESS!");
                    return _readBytes;
                }
            }

            Debug("BleWriteChar FAIL!");
            return null;
        }

        #endregion

        #region singleton

        private static BleControlV1 ins;

        private BleControlV1()
        {
        }


        public static BleControlV1 Only()
        {
            return ins ?? (ins = new BleControlV1());
        }

        #endregion
    }
}