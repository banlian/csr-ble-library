using System;
using System.Runtime.InteropServices;

namespace CsrBleLibrary.uEnergyHost
{

    static class CsrBleDll
    {

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint CsrBleHostInit(bool useLogging, string logFilename, ref CSR_BLE_TRANSPORT transport);

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleHostStart(uint threadId);

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleHostStartWnd(IntPtr hWnd);

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleHostDeinit();

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleHostStartDeviceSearch(uint searchTime);

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleHostStopDeviceSearch();



        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleHostStartLEScan();

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleHostStopLESearch();

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleHostConnect(CSR_BLE_BLUETOOTH_ADDRESS address);

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleHostUseRandomAddress(bool useRandomAddress);

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleHostCancelConnect();

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleHostSetConnectionParams(ushort scanInterval, ushort scanWindow, ushort connMin, ushort connMax, ushort latency, ushort timeout);

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleHostUpdateConnectionParams(CSR_BLE_BLUETOOTH_ADDRESS address, ushort connMin, ushort connMax, ushort latency, ushort timeout, ushort minCeLength, ushort maxCeLength);

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleHostDisconnect(uint connectHandle);

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleHostGetPairedDeviceList(ref ushort nPairedDevices, ref CSR_BLE_PAIRED_DEVICE pairedDevices);

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleHostFreePairedDeviceList(ref CSR_BLE_PAIRED_DEVICE pairedDevices);

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleHostUpdatePairedDeviceList(ref CSR_BLE_PAIRED_DEVICE pairedDevice);

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleHostFreeMessageContents(IntPtr wParam, IntPtr lParam);

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleHostCancel();

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleHostGetBtBaseUuid(ref uint pBase, int length);

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleHostGetRssi(CSR_BLE_BLUETOOTH_ADDRESS address);


        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleHostDebond(CSR_BLE_BLUETOOTH_ADDRESS device);

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleHostJustWorksResult(CSR_BLE_BLUETOOTH_ADDRESS device, bool accept, bool bond);

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleHostPasskeyNotificationResult(bool accept, CSR_BLE_BLUETOOTH_ADDRESS device);

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleHostSetEncryption(uint connectHandle, byte security);

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void CsrBleHostChangeSecurityLevel(bool mitmRequired);


        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void CsrBleHostCancelBonding(CSR_BLE_BLUETOOTH_ADDRESS address);

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleHostAcceptConnUpdate(uint connectHandle, ushort id, bool accept);

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleClientDiscoverServices(uint connectHandle);

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleClientDiscoverCharacteristics(uint connectHandle, ushort startHandle, ushort endHandle);

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleClientDiscoverCharacteristicsByUuid(uint connectHandle, CSR_BLE_UUID uuid, ushort startHandle, ushort endHandle);

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleClientDiscoverCharacteristicDsc(uint connectHandle, ushort startHandle, ushort endHandle);

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleClientDiscoverDatabase(uint connectHandle);

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleClientReadCharByHandle(uint connectHandle, ushort charHandle);

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleClientReadCharsByHandles(uint connectHandle, ushort userTag, int nHandles,ref ushort charHandle);

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleClientReadCharByUuid(uint connectHandle, CSR_BLE_UUID uuid, ushort startHandle, ushort endHandle);

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleClientWriteCharByHandle(uint connectHandle, bool isSigned, ushort charHandle, ushort valueSize, IntPtr value);

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleClientWriteCfmCharByHandle(uint connectHandle, ushort charHandle, ushort offset, ushort valueSize, IntPtr value);

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleClientWriteConfiguration(uint connectHandle, ushort charDscHandle, ushort charHandle, byte config);

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleServerAllocDatabase(ushort nHandles);

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleServerAddDatabase(ushort startHandle, ushort nServices, ref CSR_BLE_DB_SERVICE services);

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleServerReadResponse(uint connectHandle, ushort handle, ushort response, ushort length, IntPtr value);


        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleServerWriteResponse(uint connectHandle, ushort handle, ushort response);

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleServerSendNotification(uint connectHandle, ushort handle, ushort length, IntPtr value);

        [DllImport("uEnergyHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CsrBleServerSendIndication(uint connectHandle, ushort handle, ushort length, IntPtr value);


    }
}
