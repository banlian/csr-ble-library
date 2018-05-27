using System;
using System.Runtime.InteropServices;
using System.Text;

namespace CsrBleLibrary.uEnergyHost
{
    class CsrBleDefine
    {
        public const ushort MIN_CONN_INTERVAL = 0x06;
        public const ushort MAX_CONN_INTERVAL = 0x06;
        public const ushort SLAVE_LATENCY = 0x00;
        public const ushort LINK_TIMEOUT = 0x1F4;


        public const uint CSR_BT_UUID16_SIZE_IN_BYTES = 02;
        public const uint CSR_BT_UUID32_SIZE_IN_BYTES = 04;
        public const uint CSR_BT_UUID128_SIZE_IN_BYTES = 16;

        // Response codes to be used in response to
        // CSR_BLE_SERVER_DATABASE_READ_INDICATION and
        // CSR_BLE_SERVER_DATABASE_WRITE_INDICATION
        public const uint ACCESS_RESPONSE_SUCCESS = 0;
        public const uint ACCESS_ERROR_INVALID_HANDLE = 01;
        public const uint ACCESS_ERROR_READ_NOT_PERMITTED = 02;
        public const uint ACCESS_ERROR_WRITE_NOT_PERMITTED = 03;
        public const uint ACCESS_ERROR_INSUFFICIENT_ENCRYPTION = 0x0f;

        /**
         *  \defgroup common Common definitions
         *  \defgroup initialization Initialisation
         *  \defgroup messages Messages
         *  \{
         *  \defgroup host_messages Host messages
         *  \defgroup gatt_client_messages GATT Client messages
         *  \}
         *  \defgroup host GATT common API
         *  \defgroup client GATT client API
         */


        /* Top-level documentation */
        /**
         *  \addtogroup initialization
         *  \{
         */

        /**
         *  \brief Default USB device number
         *  \hideinitializer
         */
        public const uint CSR_BLE_DEFAULT_USB_DEVICE_NUMBER = 0;

        /**
         *  \name Transport type
         *  \anchor transport_type
         *  \{
         */

        /**
         *  \brief USB transport
         *  \hideinitializer
         */
        public const uint CSR_BLE_TRANSPORT_USB = 0;

        /**
         *  \brief UART transport (BCSP protocol)
         *  \hideinitializer
         */
        public const uint CSR_BLE_TRANSPORT_SERIAL_BCSP = 1;

        /**
         *  \brief UART transport (H4DS protocol)
         *  \hideinitializer
         */
        public const uint CSR_BLE_TRANSPORT_SERIAL_H4DS = 2;
        /**
         *  \}
         */

        /**
         *  \}
         */


        /**
         *  \addtogroup common
         *  \{
         */

        /**
         *  \brief      Ignore the function parameter
         */
        public const uint CSR_BLE_PARAMETER_IGNORE = 0xFFFF;

        /**
         *  \}
         */

        /**
         *  \addtogroup host_messages
         *  \{
         */

        /**
         *  \anchor     panic_reasons
         *  \name       Panic reasons
         *      \{
         */

        /**
         *  \brief      No panic
         *  \hideinitializer
         */
        public const uint CSR_BLE_PANIC_NO_PANIC = 0x0000;
        /**
         *  \brief      Transport initialisation failed
         *  \hideinitializer
         */
        public const uint CSR_BLE_PANIC_TRANSPORT_INIT_FAILURE = 0x0001;
        /**
         *  \brief      Transport communication failure
         *  \hideinitializer
         */
        public const uint CSR_BLE_PANIC_TRANSPORT_FAILURE = 0x0002;
        /**
         *  \brief      The other panic reason
         *  \hideinitializer
         */
        public const uint CSR_BLE_PANIC_UNKNOWN = 0x0003;
        /**
         *      \}
         */

        /**
         *  \anchor information_reported
         *  \name   Information reported regarding the discovered device
         *      \{
         */

        /**
         *  \brief  Shortened local name
         *  \hideinitializer
         *  \see    BLUETOOTH SPECIFICATION Version 4.0 [Vol 3], Generic Access
         *          Profile, Section 11.1.
         */
        public const uint CSR_BLE_DEVICE_SEARCH_SHORT_NAME = (0x0001) << 0;
        /**
         *  \brief  Complete local name
         *  \hideinitializer
         *  \see    BLUETOOTH SPECIFICATION Version 4.0 [Vol 3], Generic Access
         *          Profile, Section 11.1.
         */
        public const uint CSR_BLE_DEVICE_SEARCH_COMPLETE_NAME = (0x0001) << 1;
        /**
         *  \brief  Flags
         *  \hideinitializer
         *  \see    BLUETOOTH SPECIFICATION Version 4.0 [Vol 3], Generic Access
         *          Profile, Section 11.1.
         */
        public const uint CSR_BLE_DEVICE_SEARCH_FLAGS = (0x0001) << 2;
        /**
         *  \brief  Complete list of the device services
         *  \hideinitializer
         *  \see    BLUETOOTH SPECIFICATION Version 4.0 [Vol 3], Generic Access
         *          Profile, Section 11.1.
         */
        public const uint CSR_BLE_DEVICE_SEARCH_COMPLETE_SERVICES = (0x0001) << 3;
        /**
         *  \brief  Incomplete list of the device services
         *  \hideinitializer
         *  \see    BLUETOOTH SPECIFICATION Version 4.0 [Vol 3], Generic Access
         *          Profile, Section 11.1.
         */
        public const uint CSR_BLE_DEVICE_SEARCH_INCOMPLETE_SERVICES = (0x0001) << 4;
        /**
         *  \brief  TX power level
         *  \hideinitializer
         *  \see    BLUETOOTH SPECIFICATION Version 4.0 [Vol 3], Generic Access
         *          Profile, Section 11.1.
         */
        public const uint CSR_BLE_DEVICE_SEARCH_TX_LEVEL = (0x0001) << 5;
        /**
         *  \brief  Security manager OOB flags
         *  \hideinitializer
         *  \see    BLUETOOTH SPECIFICATION Version 4.0 [Vol 3], Generic Access
         *          Profile, Section 11.1.
         */
        public const uint CSR_BLE_DEVICE_SEARCH_OOB = (0x0001) << 6;
        /**
         *  \brief  Security manager TK value
         *  \hideinitializer
         *  \see    BLUETOOTH SPECIFICATION Version 4.0 [Vol 3], Generic Access
         *          Profile, Section 11.1.
         */
        public const uint CSR_BLE_DEVICE_SEARCH_TK = (0x0001) << 7;
        /**
         *  \brief  Slave connection interval range
         *  \hideinitializer
         *  \see    BLUETOOTH SPECIFICATION Version 4.0 [Vol 3], Generic Access
         *          Profile, Section 11.1.
         */
        public const uint CSR_BLE_DEVICE_SEARCH_CONNECTION_INTERVAL = (0x0001) << 8;
        /**
         *  \brief  Service solicitation
         *  \hideinitializer
         *  \todo   Currently not supported
         *  \see    BLUETOOTH SPECIFICATION Version 4.0 [Vol 3], Generic Access
         *          Profile, Section 11.1.
         */
        public const uint CSR_BLE_DEVICE_SEARCH_SERVICE_SOLICITATION = (0x0001) << 9; /* TODO: not supported */
        /**
                                                                                           *  \brief  Service data
                                                                                           *  \hideinitializer
                                                                                           *  \see    BLUETOOTH SPECIFICATION Version 4.0 [Vol 3], Generic Access
                                                                                           *          Profile, Section 11.1.
                                                                                           */
        public const uint CSR_BLE_DEVICE_SEARCH_SERVICE_DATA = (0x0001) << 10;
        /**
         *  \brief  Manufacturer specific data
         *  \hideinitializer
         *  \see    BLUETOOTH SPECIFICATION Version 4.0 [Vol 3], Generic Access
         *          Profile, Section 11.1.
         */
        public const uint CSR_BLE_DEVICE_SEARCH_MANUFACTURER = (0x0001) << 11;

        /**
         *  \brief  Appearance
         *  \hideinitializer
         *  \see    BLUETOOTH SPECIFICATION Version 4.0 [Vol 3], Generic Access
         *          Profile, Section 11.1.
         */
        public const uint CSR_BLE_DEVICE_APPEARANCE = (0x0001) << 12;

        /**
         *      \}
         */
        /*************************************************************************************
            Defines Appearance AD types values. 
            The values are composed of a category (10-bits) and sub-categories (6-bits). 
            and can be found in assigned numbers
        ************************************************************************************/
        public const uint CSR_BLE_APPEARANCE_UNKNOWN = 0x0000;
        public const uint CSR_BLE_APPEARANCE_HID_GENERIC = 0x03c0;
        public const uint CSR_BLE_APPEARANCE_HID_KEYBOARD = 0x03c1;
        public const uint CSR_BLE_APPEARANCE_HID_MOUSE = 0x03c2;
        public const uint CSR_BLE_APPEARANCE_HID_MFSW = 0x03ca;

        public const uint CSR_BLE_SECURITY_BONDING_ESTABLISHED = 0;
        public const uint CSR_BLE_SECURITY_BONDING_FAILED = 1;
        public const uint CSR_BLE_SECURITY_BONDING_TIMEOUT = 2;

        public const uint CSR_BLE_SECURITY_DEFAULT = 0;
        public const uint CSR_BLE_SECURITY_UNAUTHENTICATED = 1;
        public const uint CSR_BLE_SECURITY_AUTHENTICATED = 2;

        /**
         *  \name Messages
         *      \{
         */

        /**
         *  \brief      Host ready message
         *  \hideinitializer
         *  \details    This message is sent by the uEnergy Host library after
         *              #CsrBleHostStart() function is called and initialisation is
         *              complete. The host application should wait for this message
         *              before calling any other uEnergy functions.
         *
         *  \param      LPARAM
         *              TRUE in case of successful initialisation, FALSE otherwise.
         * *  \see        #CsrBleHostStart()
         */
        public const uint CSR_BLE_HOST_READY = 0x0000;

        /**
         *  \brief      Host panic message
         *  \hideinitializer
         *  \details    This message is sent by the uEnergy Host library in response to the
         *              critical error (such as transport failure). The host
         *              application should deinitialise the uEnergy Host library and stop
         *              all further LE functionality.
         *  \param      LPARAM
         *              \ref panic_reasons "Panic reason".
         *          
         */
        public const uint CSR_BLE_HOST_PANIC = 0x0001;

        /**
         *  \brief      Device discovery result
         *  \hideinitializer
         *  \details    These messages are sent in response to
         *              #CsrBleHostStartDeviceSearch() call. It contains information
         *              regarding the discovered device.
         *
         *  \param      LPARAM
         *              #PCSR_BLE_DEVICE_SEARCH_RESULT, pointer to
         *              #CSR_BLE_DEVICE_SEARCH_RESULT structure.
         *
         *  \see        #CsrBleHostStartDeviceSearch()\n
         *              #CSR_BLE_DEVICE_SEARCH_RESULT
         */
        public const uint CSR_BLE_HOST_SEARCH_RESULT = 0x0002;

        /**
         *  \brief      Device discovery stopped indication
         *  \hideinitializer
         *  \details    This messages is sent when device search initiated by
         *              #CsrBleHostStartDeviceSearch() call has stopped due to timeout
         *              or #CsrBleHostStopDeviceSearch() call.
         *
         *  \param      LPARAM
         *              Ignored.
         *
         *  \see        #CsrBleHostStartDeviceSearch()\n
         *              #CsrBleHostStopDeviceSearch()
         */
        public const uint CSR_BLE_HOST_SEARCH_STOPPED = 0x0003;


        /**
         *  \brief      Connection indication
         *  \hideinitializer
         *  \details    This messages indicates the result of the connection request.
         *
         *  \param      LPARAM
         *              #PCSR_BLE_CONNECT_RESULT, pointer to
         *              #CSR_BLE_CONNECT_RESULT structure.
         *
         *  \see        #CsrBleHostConnect()\n
         *              #CsrBleHostCancelConnect()\n
         *              #CSR_BLE_CONNECT_RESULT
         */
        public const uint CSR_BLE_HOST_CONNECT_RESULT = 0x0004;

        /**
         *  \brief      Disconnection indication
         *  \hideinitializer
         *  \details    This messages is sent to the application when disconnection
         *              is detected. The disconnection could be triggered locally by
         *              call to #CsrBleHostDisconnect().
         *
         *  \param      LPARAM
         *              #PCSR_BLE_DISCONNECTED, pointer to
         *              #CSR_BLE_DISCONNECTED structure.
         *
         *  \see        #CsrBleHostDisconnect()\n
         *              #CSR_BLE_DISCONNECTED
         */
        public const uint CSR_BLE_HOST_DISCONNECTED = 0x0005;

        /**
         *  \brief      Connection parameters updated
         *  \hideinitializer
         *  \details    This messages indicates the result of the connection parameter
         *              update request sent by #CsrBleHostUpdateConnectionParams().
         *
         *  \param      LPARAM
         *              Result of the operation. <b>0</b>, in case of success.
         *
         *  \see        #CsrBleHostSetConnectionParams()
         *              #CsrBleHostUpdateConnectionParams()
         */
        public const uint CSR_BLE_HOST_CONN_PARAM_UPDATE = 0x0006;

        /**
         *  \brief      RSSI measurement
         *  \hideinitializer
         *  \details    This messages is sent in response to #CsrBleHostGetRssi()
         *              call with the measured RSSI value for the specified device.
         *
         *  \param      LPARAM
         *              #PCSR_BLE_RSSI_RESULT, pointer to #CSR_BLE_RSSI_RESULT
         *              structure.
         *
         *  \see        #CsrBleHostGetRssi()
         */
        public const uint CSR_BLE_HOST_RSSI_RESULT = 0x0007;

        /**
         *  \brief      Debond Result
         *  \hideinitializer
         *  \details    This messages is sent in response to #CsrBleHostDebond()
         *              call with the address of the specified device.
         *
         *  \param      LPARAM
         *              #PCSR_BLE_DEBOND_RESULT, pointer to #CSR_BLE_DEBOND_RESULT
         *              structure.
         *
         *  \see        #CsrBleHostDebond()
         */
        public const uint CSR_BLE_HOST_DEBOND_RESULT = 0x0008;

        /**
         *  \brief      Just Works Request
         *  \hideinitializer
         *  \details    Request for accepting a pairing attempt. The application may respond 
         *              to the request or may forward the request to the user, e.g. through 
         *              the MMI. Note: This primitive is only applicable when both the local 
         *              and remote device supports SSP and MITM is not required by both parties.
         *
         *  \param      LPARAM
         *              #PCSR_BLE_JUSTWORKS_REQUEST, pointer to #CSR_BLE_JUSTWORKS_REQUEST
         *              structure.
         *
         *  \see        #CsrBleHostSetEncryption()
         */
        public const uint CSR_BLE_HOST_JUSTWORKS_REQUEST = 0x0009;

        /**
         *  \brief      Security/Pairing Indication
         *  \hideinitializer
         *  \details    This indication is sent to the registered application whenever a Low
         *              Energy security procedure has completed either successfully or if the 
         *              procedure failed.
         *
         *  \param      LPARAM
         *              #PCSR_BLE_SECURITY_RESULT, pointer to #CSR_BLE_SECURITY_RESULT
         *              structure.
         *
         *  \see        #CsrBleHostSetEncryption()
         */
        public const uint CSR_BLE_HOST_SECURITY_RESULT = 0x000A;

        /**
         *  \brief      Request LE Authentication Procedure
         *  \hideinitializer
         *  \details    This is a confirmation to the initiation of the security request from  
         *              the application.
         *
         *  \param      LPARAM
         *              #PCSR_BLE_SET_ENCRYPTION_RESULT, pointer to #CSR_BLE_SET_ENCRYPTION_RESULT
         *              structure.
         *
         *  \see        #CsrBleHostSetEncryption()
         */
        public const uint CSR_BLE_HOST_SET_ENCRYPTION_RESULT = 0x000B;

        /**
         *  \brief      Connection Parameter UpdateDetails Request
         *  \hideinitializer
         *  \details    The application will receive a CSR_BLE_HOST_CONNECTION_UPDATE_REQUEST 
         *              message whenever another local application or a peer Slave requests to 
         *              update the connection parameter into an interval that is outside the 
         *              range of which it has registered to GATT.
         *
         *  \param      LPARAM
         *              #PCSR_BLE_CONNECTION_UPDATE_REQUEST, pointer to 
         *              #CSR_BLE_CONNECTION_UPDATE_REQUEST structure.
         *
         *  \see        #CsrBleHostSetConnectionParams()
         */
        public const uint CSR_BLE_HOST_CONNECTION_UPDATE_REQUEST = 0x000C;

        /**
         *  \brief      Connection Parameter UpdateDetails Indication
         *  \hideinitializer
         *  \details    The application will receive a CSR_BLE_HOST_CONNECTION_UPDATED 
         *              message whenever the actual connection parameters have changed. 
         *
         *  \param      LPARAM
         *              #PCSR_BLE_CONNECTION_UPDATED, pointer to 
         *              #CSR_BLE_CONNECTION_UPDATED structure.
         *
         *  \see        #CsrBleHostSetConnectionParams()
         */
        public const uint CSR_BLE_HOST_CONNECTION_UPDATED = 0x000D;

        /**
         *  \brief      The indication contains a passkey that must be shown on the display 
         *              so that the remote side will be able to enter the same passkey in the
         *              responding side.  
         *
         *  \param      LPARAM
         *              #PCSR_BLE_DISPLAY_PASSKEY_IND, pointer to
         *              #CSR_BLE_DISPLAY_PASSKEY_IND structure.
         *
         *  \see        #CsrBleHostPasskeyNotificationResult()\n
         */
        public const uint CSR_BLE_HOST_DISPLAY_PASSKEY_IND = 0x000E;

        /**
         *  \brief      The indication is confirmation to the GATT scan request initiated 
         *              by the application.
         *
         *  \param      LPARAM
         *              #PCSR_BLE_MSG_NOTIFY, pointer to
         *              #CSR_BLE_MSG_NOTIFY structure.
         *
         *  \see        #CsrBleHostStartLEScan()\n
         */
        public const uint CSR_BLE_HOST_LE_SCAN_STATUS = 0x000F;


        /**
         *  \brief      This is confirmation to any GATT event send request initiated 
         *              by the application.
         *
         *  \param      LPARAM
         *              #PCSR_BLE_MSG_NOTIFY, pointer to
         *              #CSR_BLE_MSG_NOTIFY structure.
         *
         *  \see        #CsrBleHostStartLEScan()\n
         */
        public const uint CSR_BLE_HOST_LE_EVENT_SEND_STATUS = 0x0010;


        /**
         *  \brief      Invalid connection handle
         */
        public const uint CSR_BLE_INVALID_HANDLE = 0xFFFF;

        /**
         *  \brief      No updates
         */
        public const uint CSR_BLE_NONE = 0x00;
        /**
         *  \brief      Notification
         */
        public const uint CSR_BLE_NOTIFICATION = 0x01;
        /**
         *  \brief      Indication
         */
        public const uint CSR_BLE_INDICATION = 0x02;

        /**
         *  \brief      Discovered primary service
         *  \hideinitializer
         *  \details    This messages is sent with the information of the
         *              discovered primary service.
         *
         *  \param      LPARAM
         *              #PCSR_BLE_SERVICE_DISCOVERY_RESULT, pointer to
         *              #CSR_BLE_SERVICE_DISCOVERY_RESULT structure.
         *
         *  \see        #CsrBleClientDiscoverServices()
         */
        public const uint CSR_BLE_CLIENT_SERVICE_DISCOVERY_RESULT = 0x0100;

        /**
         *  \brief      Result of the primary service discovery
         *  \hideinitializer
         *  \details    This messages is sent when all primary services were discovered
         *              or when discovery failed.
         *
         *  \param      LPARAM
         *              #PCSR_BLE_SERVICE_DISCOVERY_STOPPED, pointer to
         *              #CSR_BLE_SERVICE_DISCOVERY_STOPPED structure.
         *
         *  \see        #CsrBleClientDiscoverServices()
         */
        public const uint CSR_BLE_CLIENT_SERVICE_DISCOVERY_STOPPED = 0x0101;

        /**
         *  \brief      Discovered service characteristic
         *  \hideinitializer
         *  \details    This messages is sent with the information of the
         *              discovered service characteristic.
         *
         *  \param      LPARAM
         *              #PCSR_BLE_CHAR_DISCOVERY_RESULT, pointer to
         *              #CSR_BLE_CHAR_DISCOVERY_RESULT structure.
         *
         *  \see        #CsrBleClientDiscoverCharacteristics()\n
         *              #CsrBleClientDiscoverCharacteristicsByUuid()
         */
        public const uint CSR_BLE_CLIENT_CHAR_DISCOVERY_RESULT = 0x0102;

        /**
         *  \brief      Result of service characteristics discovery
         *  \hideinitializer
         *  \details    This messages is sent when all characteristics of the service
         *              were discovered or when discovery failed.
         *
         *  \param      LPARAM
         *              #PCSR_BLE_CHAR_DISCOVERY_STOPPED, pointer to
         *              #CSR_BLE_CHAR_DISCOVERY_STOPPED structure.
         *
         *  \see        #CsrBleClientDiscoverCharacteristics()\n
         *              #CsrBleClientDiscoverCharacteristicsByUuid()
         */
        public const uint CSR_BLE_CLIENT_CHAR_DISCOVERY_STOPPED = 0x0103;

        /**
         *  \brief      Discovered characteristic descriptor
         *  \hideinitializer
         *  \details    This messages is sent with the information of the
         *              discovered characteristic descriptor.
         *
         *  \param      LPARAM
         *              #PCSR_BLE_CHAR_DSC_DISCOVERY_RESULT, pointer to
         *              #CSR_BLE_CHAR_DSC_DISCOVERY_RESULT structure.
         *
         *  \see        #CsrBleClientDiscoverCharacteristicDsc()
         */
        public const uint CSR_BLE_CLIENT_CHAR_DSC_DISCOVERY_RESULT = 0x0104;

        /**
         *  \brief      Result of characteristic descriptors discovery
         *  \hideinitializer
         *  \details    This messages is sent when all descriptor of the
         *              characteristic were discovered or when discovery failed.
         *
         *  \param      LPARAM
         *              #PCSR_BLE_CHAR_DSC_DISCOVERY_STOPPED, pointer to
         *              #CSR_BLE_CHAR_DSC_DISCOVERY_STOPPED structure.
         *
         *  \see        #CsrBleClientDiscoverCharacteristicDsc()
         */
        public const uint CSR_BLE_CLIENT_CHAR_DSC_DISCOVERY_STOPPED = 0x0105;

        /**
         *  \brief      Discovered GATT database
         *  \hideinitializer
         *  \details    This messages is sent when all GATT database of the remote GATT
         *              server is read or when discovery operation fails.
         *
         *  \param      LPARAM
         *              #PCSR_BLE_DATABASE_DISCOVERY_RESULT, pointer to
         *              #CSR_BLE_DATABASE_DISCOVERY_RESULT structure.
         *
         *  \see        #CsrBleClientDiscoverDatabase()
         */
        public const uint CSR_BLE_CLIENT_DATABASE_DISCOVERY_RESULT = 0x0106;

        /**
         *  \brief      Read characteristic value
         *  \hideinitializer
         *  \details    This messages is sent with the value of the read
         *              characteristic.
         *
         *  \param      LPARAM
         *              #PCSR_BLE_CHAR_READ_RESULT, pointer to
         *              #CSR_BLE_CHAR_READ_RESULT structure.
         *
         *  \see        #CsrBleClientReadCharByHandle()\n
         *              #CsrBleClientReadCharByUuid()
         */
        public const uint CSR_BLE_CLIENT_CHAR_READ_RESULT = 0x0107;

        /**
         *  \brief      Read characteristics values
         *  \hideinitializer
         *  \details    This messages is sent with the values of the read
         *              characteristics.
         *
         *  \param      LPARAM
         *              #PCSR_BLE_CHAR_READ_MULTI_RESULT, pointer to
         *              #CSR_BLE_CHAR_READ_MULTI_RESULT structure.
         *
         *  \see        #CsrBleClientReadCharsByHandles()
         */
        public const uint CSR_BLE_CLIENT_CHAR_READ_MULTI_RESULT = 0x0108;

        /**
         *  \brief      Characteristic write result
         *  \hideinitializer
         *  \details    This messages is sent to indicate local or remote write result.
         *
         *  \param      LPARAM
         *              #PCSR_BLE_WRITE_RESULT, pointer to
         *              #CSR_BLE_WRITE_RESULT structure.
         *
         *  \see        #CsrBleClientWriteCharByHandle()\n
         *              #CsrBleClientWriteCfmCharByHandle()
         */
        public const uint CSR_BLE_CLIENT_CHAR_WRITE_RESULT = 0x0109;

        /**
         *  \brief      Incoming GATT server notification or indication
         *  \hideinitializer
         *  \details    This messages is sent by the uEnergy Host library when remote
         *              GATT server sends notification or indication to the
         *              connected client.
         *
         *  \param      LPARAM
         *              #PCSR_BLE_CHAR_NOTIFICATION, pointer to
         *              #CSR_BLE_CHAR_NOTIFICATION structure.
         *
         *  \see        #CsrBleClientWriteConfiguration()
         */
        public const uint CSR_BLE_CLIENT_CHAR_NOTIFICATION = 0x010A;

        public const uint CSR_BLE_SERVER_ALLOC_DATABASE_RESULT = 0x0200;
        public const uint CSR_BLE_SERVER_ADD_DATABASE_RESULT = 0x0201;
        public const uint CSR_BLE_SERVER_DATABASE_READ_INDICATION = 0x0202;
        public const uint CSR_BLE_SERVER_DATABASE_WRITE_INDICATION = 0x0203;
    }


    /**
     *  \brief Transport configuration
     *  This structure provides transport configuration for #CsrBleHostInit()
     */

    public struct CSR_BLE_TRANSPORT
    {
        public byte transportType; /**< Transport type (use one of the \ref transport_type transport constants) */

        /**
         *  \name USB transport configuration
         *  \{
         */

        public byte usbDeviceNumber;
            /**< USB device number (use #CSR_BLE_DEFAULT_USB_DEVICE_NUMBER for default GattDevice) */

        /**
         *  \}
         */

        /**
         *  \name UART transport configuration
         *  \{
         */
        public uint serialBaudRate; /**< Serial baud rate */
        public uint serialResetBaudRate; /**< Reset serial baud rate */
        public byte serialComPort; /**< COM port number */
        /**
         *  \}
         */
    }

    /**
     *  \brief Bluetooth address
     */

    public struct CSR_BLE_BLUETOOTH_ADDRESS
    {
        public byte type; /* Type of Bluetooth address (Public or random) */
        public ushort nAp; /**< Non-significant part of Bluetooth address [47..32] */
        public byte uAp; /**< Upper part of Bluetooth address [31..24] */
        public uint lAp; /**< Lower part of Bluetooth address [23..0] */

        public override string ToString()
        {
            return string.Format("[{0:X2}-{1:X4}-{2:X2}-{3:X6}]", type, nAp, uAp, lAp);
        }
    }

    /**
     *  \brief UUID
     */

    public struct CSR_BLE_UUID
    {
        public byte lengthInBytes; /* returns the length of the UUID in public byte s */
        public ushort uuid16; /**< 16-bit UUID */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)] public byte[] uuid128; /**< 128-bit UUID */


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("[{0:X4}]-[", uuid16));
            for (int i = 0; i < uuid128.Length; i++)
            {
                sb.Append(string.Format("{0:X2}", uuid128[i]));
            }
            sb.Append("]");
            return sb.ToString();
        }
    }

    /**
     *  \brief  Paired device description
     *  \details    This structure describes paired device
     *
     *  \see
     */

    public struct CSR_BLE_PAIRED_DEVICE
    {
        /**
         *  Device address
         */
        public CSR_BLE_BLUETOOTH_ADDRESS deviceAddress;

        /**
         *  Device name
         */
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)] public string deviceName;

        /**
         *  Device appearance value
         */
        public ushort appearance;
    }


    /**
     *  \brief      GATT characteristic descriptor description
     *  \details    This structure describes GATT characteristic descriptor.
     *
     *  \see        #CsrBleClientDiscoverDatabase()\n
     *              #CSR_BLE_CHARACTERISTIC\n
     *              #CSR_BLE_SERVICE\n
     */

    public struct CSR_BLE_CHARACTERISTIC_DSC
    {
        /**
         *  Characteristic descriptor UUID
         */
        public CSR_BLE_UUID uuid;
        /**
         *  Characteristic descriptor value handle
         */
        public ushort handle;
    }


    /**
     *  \brief      GATT characteristic description
     *  \details    This structure describes GATT characteristic.
     *
     *  \see        #CsrBleClientDiscoverDatabase()\n
     *              #CSR_BLE_SERVICE\n
     *              #CSR_BLE_CHARACTERISTIC_DSC
     */

    public struct CSR_BLE_CHARACTERISTIC
    {
        /**
         *  Characteristic UUID
         */
        public CSR_BLE_UUID uuid;
        /**
         *  Characteristic declaration handle
         */
        public ushort declHandle;
        /**
         *  Characteristic value handle
         */
        public ushort handle;
        /**
         *  Characteristic properties
         */
        public byte properties;

        /**
         *  Number of characteristic descriptors
         */
        public ushort nDescriptors;
        /**
         *  Characteristic descriptors
         */
        public IntPtr descriptors;
    }


    /**
     *  \brief      GATT service description
     *  \details    This structure describes GATT service.
     *
     *  \see        #CsrBleClientDiscoverDatabase()\n
     *              #CSR_BLE_CHARACTERISTIC\n
     *              #CSR_BLE_CHARACTERISTIC_DSC
     */

    public struct CSR_BLE_SERVICE
    {
        /**
         *  Service UUID
         */
        public CSR_BLE_UUID uuid;
        /**
         *  First handle of the service
         */
        public ushort startHandle;
        /**
         *  Last handle of the service
         */
        public ushort endHandle;

        /**
         *  Number of characteristics in this service
         */
        public ushort nCharacteristics;
        /**
         *  Characteristics in this service
         */
        public IntPtr characteristics;
    }


    public struct CSR_BLE_DB_CHARACTERISTIC
    {
        public CSR_BLE_UUID uuid; /* characteristic UUID */
        public byte properties; /* properties */
        public ushort permission; /* permisssion bits */
        public ushort flags; /* flags */

        public ushort handle;
        public ushort clientCfgHandle;

        public ushort length; /* value length */
        public IntPtr value; /* value */
    }

    public struct CSR_BLE_DB_SERVICE
    {
        public CSR_BLE_UUID uuid; /* Service UUID */

        public ushort handle;

        public ushort nCharacteristics; /* number of characteristics */
        public IntPtr characteristics; /* service characteristics */
    }

    /**
     *  \brief      Discovered device
     *  \details    This structure contains information regarding the discovered device.
     *              It is returned with the #CSR_BLE_HOST_SEARCH_RESULT message.
     */

    public struct CSR_BLE_DEVICE_SEARCH_RESULT
    {
        public CSR_BLE_BLUETOOTH_ADDRESS deviceAddress; /**< Device address */

        public sbyte rssi; /**< Device RSSI */

        /**
         *  Bitmask of the reported data, combination of the \ref information_reported
         *  "information reported flags".
         */
        public ushort informationReported;


        /**
         *  Device local name, either shortened or complete.
         *  \note   This field contains valid information when one of the
         *          #CSR_BLE_DEVICE_SEARCH_SHORT_NAME or
         *          #CSR_BLE_DEVICE_SEARCH_COMPLETE_NAME bits is set in the
         *          #informationReported field.
         */
        public string deviceName;

        /**
         *  Device flags.
         *  \note   This field contains valid information when
         *          #CSR_BLE_DEVICE_SEARCH_FLAGS bit is set in the
         *          #informationReported field.
         *  \see    BLUETOOTH SPECIFICATION Version 4.0 [Vol 3], Generic Access
         *          Profile, Section 18.1.
         */
        public byte flags;

        /**
         *  Device advertised TX Power level.
         *  \note   This field contains valid information when
         *          #CSR_BLE_DEVICE_SEARCH_TX_LEVEL bit is set in the
         *          #informationReported field.
         *  \see    BLUETOOTH SPECIFICATION Version 4.0 [Vol 3], Generic Access
         *          Profile, Section 18.4.
         */
        public sbyte txPowerLevel;

        /**
     *  Device advertised appearance value.
     *  \note   This field contains valid information when
     *          #CSR_BLE_DEVICE_APPEARANCE bit is set in the
     *          #informationReported field.
     *  \see    BLUETOOTH SPECIFICATION Version 4.0 [Vol 3], Generic Access
     *          Profile, Section 12.2.
     */
        public ushort appearance;
        /**
         *  Number of the advertised device primary services.
         *  \note   This field contains valid information when either
         *          #CSR_BLE_DEVICE_SEARCH_COMPLETE_SERVICES or
         *          #CSR_BLE_DEVICE_SEARCH_INCOMPLETE_SERVICES bit is set in the
         *          #informationReported field.
         *  \see    #deviceServices
         */
        public byte nDeviceServices;
        /**
         *  Complete or incomplete UUID list of the device primary services.
         *  \note   This field contains valid information when either
         *          #CSR_BLE_DEVICE_SEARCH_COMPLETE_SERVICES or
         *          #CSR_BLE_DEVICE_SEARCH_INCOMPLETE_SERVICES bit is set in the
         *          #informationReported field.
         *  \see    #nDeviceServices
         */
        public IntPtr deviceServices;

        /**
         *  Security manager OOB flags.
         *  \note   This field contains valid information when
         *          #CSR_BLE_DEVICE_SEARCH_OOB bit is set in the
         *          #informationReported field.
         *  \see    BLUETOOTH SPECIFICATION Version 4.0 [Vol 3], Generic Access
         *          Profile, Section 18.5.
         */
        public byte oobFlags;

        /**
         *  Security manager TK value.
         *  \note   This field contains valid information when
         *          #CSR_BLE_DEVICE_SEARCH_TK bit is set in the
         *          #informationReported field.
         *  \see    BLUETOOTH SPECIFICATION Version 4.0 [Vol 3], Generic Access
         *          Profile, Section 18.6.
         */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] public uint[] oobTk;

        /**
         *  Minimum value of the slave connection interval range.
         *  \note   This field contains valid information when
         *          #CSR_BLE_DEVICE_SEARCH_CONNECTION_INTERVAL bit is set in the
         *          #informationReported field.
         *  \see    #connIntervalMax\n
         *          BLUETOOTH SPECIFICATION Version 4.0 [Vol 3], Generic Access
         *          Profile, Section 18.8.
         */
        public ushort connIntervalMin;
        /**
         *  Maximum value of the slave connection interval range.
         *  \note   This field contains valid information when
         *          #CSR_BLE_DEVICE_SEARCH_CONNECTION_INTERVAL bit is set in the
         *          #informationReported field.
         *  \see    #connIntervalMin\n
         *          BLUETOOTH SPECIFICATION Version 4.0 [Vol 3], Generic Access
         *          Profile, Section 18.8.
         */
        public ushort connIntervalMax;

        public byte nServiceSolicitation; /* number of solicited services */
        public IntPtr serviceSolicitation; /* solicited services */

        /**
         *  Service-specific data size
         *  \note   This field contains valid information when
         *          #CSR_BLE_DEVICE_SEARCH_SERVICE_DATA bit is set in the
         *          #informationReported field.
         *  \see    #serviceUuid\n
         *          #serviceData
         */
        public byte serviceDataSize;
        /**
         *  Service 16-bit UUID
         *  \note   This field contains valid information when
         *          #CSR_BLE_DEVICE_SEARCH_SERVICE_DATA bit is set in the
         *          #informationReported field.
         *  \see    #serviceDataSize\n
         *          #serviceData
         */
        public ushort serviceUuid;

        /**
         *  Service 128-bit UUID
         *  \note   This field contains valid information when
         *          #CSR_BLE_DEVICE_SEARCH_SERVICE_DATA bit is set in the
         *          #informationReported field.
         *  \see    #serviceDataSize\n
         *          #serviceData
         */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)] public byte[] service128Uuid;
        /**
         *  Service-specific data
         *  \note   This field contains valid information when
         *          #CSR_BLE_DEVICE_SEARCH_SERVICE_DATA bit is set in the
         *          #informationReported field.
         *  \see    #serviceUuid\n
         *          #serviceDataSize
         */
        public IntPtr serviceData;

        /**
         *  Manufacturer specific data size
         *  \note   This field contains valid information when
         *          #CSR_BLE_DEVICE_SEARCH_MANUFACTURER bit is set in the
         *          #informationReported field.
         *  \see    #manufCode\n
         *          #manufData
         */
        public byte manufDataSize;
        /**
         *  Manufacturer assigned ID
         *  \note   This field contains valid information when
         *          #CSR_BLE_DEVICE_SEARCH_MANUFACTURER bit is set in the
         *          #informationReported field.
         *  \see    #manufDataSize\n
         *          #manufData
         */
        public ushort manufCode;
        /**
         *  Manufacturer specific data
         *  \note   This field contains valid information when
         *          #CSR_BLE_DEVICE_SEARCH_MANUFACTURER bit is set in the
         *          #informationReported field.
         *  \see    #manufDataSize\n
         *          #manufCode
         */
        public IntPtr manufData;
    }

    /**
     *  \brief      Connection result
     *  \details    This structure is returned with the
     *              #CSR_BLE_HOST_CONNECT_RESULT message.
     */

    public struct CSR_BLE_CONNECT_RESULT
    {
        /**
        *  Discovery result supplier, <b>0</b> means success.
        */
        public ushort supplier;
        /**
         *  Connection result. <b>0</b> means success.
         */
        public ushort result;

        /**
         *  Connection handle. The application needs to provide this handle for all
         *  operations during this connection.
         */
        public uint connectHandle;
    }


    /**
     *  \brief      Disconnection indication
     *  \details    This structure is returned with the
     *              #CSR_BLE_HOST_DISCONNECTED message.
     */

    public struct CSR_BLE_DISCONNECTED
    {
        /**
        *  Discovery result supplier, <b>0</b> means success.
        */
        public ushort supplier;
        /**
         *  Disconnection reason, <b>0</b> means success
         */
        public ushort reason;
        /**
         *  Handle of the disconnected connection
         */
        public uint connectHandle;
    }


    /**
     *  \brief      RSSI measured value
     *  \details    This structure is returned with the
     *              #CSR_BLE_HOST_RSSI_RESULT message.
     */

    public struct CSR_BLE_RSSI_RESULT
    {
        /**
        *  Discovery result supplier, <b>0</b> means success.
        */
        public ushort supplier;
        /**
         *  Measurement result code. <b>0</b> means successful measurement.
         */
        public ushort result;
        /**
         *  Device Bluetooth address
         */
        public CSR_BLE_BLUETOOTH_ADDRESS deviceAddress;
        /**
         *  Measured RSSI value
         */
        public sbyte rssi;
    }

    /**
     *  \brief      Result of Debonding
     *  \details    This structure is returned with the
     *              #CSR_BLE_DEBOND_RESULT  message.
     */

    public struct CSR_BLE_DEBOND_RESULT
    {
        /**
        *  Discovery result supplier, <b>0</b> means success.
        */
        public ushort supplier;
        /**
         *  Connection result. <b>0</b> means success.
         */
        public ushort result;
        /**
         *  Address of the disconnected device
         */
        public CSR_BLE_BLUETOOTH_ADDRESS deviceAddress;
    }

    /**
     *  \brief      Result of Just Works
     *  \details    This structure is returned with the
     *              #CSR_BLE_JUSTWORKS_REQUEST  message.
     */

    public struct CSR_BLE_JUSTWORKS_REQUEST
    {
        public CSR_BLE_BLUETOOTH_ADDRESS deviceAddress;
        public string deviceName;
        public bool paired;
    }

    /**
     *  \brief      Display passkey to be entered on the remote device
     *  \details    This structure is returned with the
     *              #CSR_BLE_HOST_DISPLAY_PASSKEY_IND message.
     */

    public struct CSR_BLE_DISPLAY_PASSKEY_IND
    {
        /**
         *  Bluetooth address of the remote device
         */
        public CSR_BLE_BLUETOOTH_ADDRESS deviceAddress;
        /**
         *  Numeric value to be displayed to the user.
         */
        public uint numericValue;

        public ushort appQId;
    }


    public struct CSR_BLE_SECURITY_RESULT
    {
        public CSR_BLE_BLUETOOTH_ADDRESS deviceAddress;
        /**
       *  Discovery result supplier, <b>0</b> means success.
       */
        public ushort supplier;
        public ushort result;
    }

    public struct CSR_BLE_SET_ENCRYPTION_RESULT
    {
        public uint connectHandle;
        /**
       *  Discovery result supplier, <b>0</b> means success.
       */
        public ushort supplier;
        public ushort result;
    }

    /**
     *  \brief      Connection UpdateDetails Request
     *  \details    This structure is filled with the
     *              #CSR_BLE_CONNECTION_UPDATE_REQUEST  message.
     */

    public struct CSR_BLE_CONNECTION_UPDATE_REQUEST
    {
        public uint connectHandle;
        public ushort conIntervalMin;
        public ushort conIntervalMax;
        public ushort conLatency;
        public ushort conSupTimeout;
        public ushort id;
    }

    public struct CSR_BLE_CONNECTION_UPDATED
    {
        public uint connectHandle;
        public ushort conInterval;
        public ushort conLatency;
        public ushort conSupTimeout;
    }

    /**
     *  \}
     */

    /**
     *  \addtogroup gatt_client_messages
     *  \{
     */

    /**
     *  \brief      Discovered primary service 
     *  \details    This structure is returned with the
     *              #CSR_BLE_CLIENT_SERVICE_DISCOVERY_RESULT message.
     */

    public struct CSR_BLE_SERVICE_DISCOVERY_RESULT
    {
        /**
         *  Connection handle
         */
        public uint connectHandle;
        /**
         *  Handle of the first attribute in discovered service
         */
        public ushort startHandle;
        /**
         *  Handle of the last attribute in discovered service
         */
        public ushort endHandle;
        /**
         *  Service UUID
         */
        public CSR_BLE_UUID deviceService;
    }

    /**
     *  \brief      Primary service discovery finished
     *  \details    This structure is returned with the
     *              #CSR_BLE_CLIENT_SERVICE_DISCOVERY_STOPPED message.
     */

    public struct CSR_BLE_SERVICE_DISCOVERY_STOPPED
    {
        /**
        *  Discovery result supplier, <b>0</b> means success.
        */
        public ushort supplier;
        /**
         *  Status of the service discovery, <b>0</b> means successful stop
         */
        public ushort result;
        /**
         *  Connection handle
         */
        public uint connectHandle;
    }

    /**
     *  \brief      Discovered service characteristic
     *  \details    This structure is returned with the
     *              #CSR_BLE_CLIENT_CHAR_DISCOVERY_RESULT message.
     */

    public struct CSR_BLE_CHAR_DISCOVERY_RESULT
    {
        /**
         *  Connection handle
         */
        public uint connectHandle;
        /**
         *  Characteristic UUID
         */
        public CSR_BLE_UUID uuid;
        /**
         *  Attribute handle to the characteristic value
         */
        public ushort valueHandle;
        /**
         *  Characteristic properties
         */
        public byte properties;
    }

    /**
     *  \brief      Characteristic discovery finished
     *  \details    This structure is returned with the
     *              #CSR_BLE_CLIENT_CHAR_DISCOVERY_STOPPED message.
     */

    public struct CSR_BLE_CHAR_DISCOVERY_STOPPED
    {
        /**
        *  Discovery result supplier, <b>0</b> means success.
        */
        public ushort supplier;
        /**
         *  Status of the service discovery, <b>0</b> means successful stop
         */
        public ushort result;
        /**
         *  Connection handle
         */
        public uint connectHandle;
    }

    /**
     *  \brief      Discovered characteristic descriptor
     *  \details    This structure is returned with the
     *              #CSR_BLE_CLIENT_CHAR_DSC_DISCOVERY_RESULT message.
     */

    public struct CSR_BLE_CHAR_DSC_DISCOVERY_RESULT
    {
        /**
         *  Connection handle
         */
        public uint connectHandle;
        /**
         *  Descriptor UUID
         */
        public CSR_BLE_UUID uuid;
        /**
         * Descriptor value attribute handle
         */
        public ushort dscHandle;
    }

    /**
     *  \brief      Characteristic descriptors discovery finished
     *  \details    This structure is returned with the
     *              #CSR_BLE_CLIENT_CHAR_DSC_DISCOVERY_STOPPED message.
     */

    public struct CSR_BLE_CHAR_DSC_DISCOVERY_STOPPED
    {
        /**
        *  Discovery result supplier, <b>0</b> means success.
        */
        public ushort supplier;
        /**
         *  Discovery result, <b>0</b> means success
         */
        public ushort result;
        /**
         *  Connection handle
         */
        public uint connectHandle;
    }

    /**
     *  \brief      Discovered GATT database
     *  \details    This structure is returned with the
     *              #CSR_BLE_CLIENT_DATABASE_DISCOVERY_RESULT message.
     */

    public struct CSR_BLE_DATABASE_DISCOVERY_RESULT
    {
        /**
         *  Connection handle
         */
        public uint connectHandle;

        /**
        *  Discovery result supplier, <b>0</b> means success.
        */
        public ushort supplier;

        /**
         *  Discovery result, <b>0</b> means success.
         */
        public ushort result;

        /**
         *  Number of services discovered
         */
        public ushort nServices;
        /**
         *  Discovered services
         */
        public IntPtr services;
    }

    /**
     *  \brief      Read characteristic value
     *  \details    This structure is returned with the
     *              #CSR_BLE_CLIENT_CHAR_READ_RESULT message.
     */

    public struct CSR_BLE_CHAR_READ_RESULT
    {
        /**
        *  Discovery result supplier, <b>0</b> means success.
        */
        public ushort supplier;
        /**
         *  Read result, <b>0</b> means success
         */
        public ushort result;
        /**
         *  Connection handle
         */
        public uint connectHandle;
        /**
         *  Characteristic value handle
         */
        public ushort charHandle;
        /**
         *  Characteristic value size in octets
         */
        public ushort charValueSize;
        /**
         *  Characteristic value
         */
        public IntPtr charValue;
    }

    /**
     *  \brief      Read characteristics values
     *  \details    This structure is returned with the
     *              #CSR_BLE_CLIENT_CHAR_READ_RESULT message.
     */

    public struct CSR_BLE_CHAR_READ_MULTI_RESULT
    {
        /**
        *  Discovery result supplier, <b>0</b> means success.
        */
        public ushort supplier;
        /**
         *  Read result, <b>0</b> means success
         */
        public ushort result;
        /**
         *  Connection handle
         */
        public uint connectHandle;
        /**
         *  User tag, specified in #CsrBleClientReadCharsByHandles() call
         */
        public ushort userTag;
        /**
         *  Combined characteristics values size
         */
        public ushort charValueSize;
        /**
         *  Combined characteristic values, one after another as specified in request
         */
        public IntPtr charValue;
    }

    /**
     *  \brief      Write confirmation
     *  \details    This structure is returned with the
     *              #CSR_BLE_CLIENT_CHAR_WRITE_RESULT message.
     */

    public struct CSR_BLE_WRITE_RESULT
    {
        /**
        *  Discovery result supplier, <b>0</b> means success.
        */
        public ushort supplier;
        /**
         *  Characteristic write result, <b>0</b> means success
         */
        public ushort result;
        /**
         *  Connection handle
         */
        public uint connectHandle;
        /**
         *  Characteristic handle
         */
        public ushort charHandle;
    }

    /**
     *  \brief      GATT server notification or indication
     *  \details    This structure is returned with the
     *              #CSR_BLE_CLIENT_CHAR_NOTIFICATION message.
     */

    public struct CSR_BLE_CHAR_NOTIFICATION
    {
        /**
         *  Connection handle
         */
        public uint connectHandle;
        /**
         *  GATT server Bluetooth address
         */
        public CSR_BLE_BLUETOOTH_ADDRESS deviceAddress;

        /**
         *  Characteristic handle
         */
        public ushort charHandle;
        /**
         *  Notified value size
         */
        public ushort charValueSize;
        /**
         *  Notified value
         */
        public IntPtr charValue;
    }

    public struct CSR_BLE_DB_ALLOC_RESULT
    {
        /**
        *  Discovery result supplier, <b>0</b> means success.
        */
        public ushort supplier;
        public ushort result; /* result */

        public ushort start; /* start handle */
        public ushort end; /* end offset */
    }

    public struct CSR_BLE_DB_READ_ACCESS_IND
    {
        public uint connectHandle; /* connection handle */

        public ushort handle; /* attribute handle */
        public ushort offset; /* attribute offset */
        public ushort maximumLength; /* maximum response length */
    }

    public struct CSR_BLE_DB_WRITE_ACCESS_PAIR
    {
        public ushort handle; /* attribute handle */
        public ushort offset; /* attribute offset */
        public ushort length; /* value length */
        public IntPtr value; /* value */
    }

    public struct CSR_BLE_DB_WRITE_ACCESS_IND
    {
        public uint connectHandle; /* connection handle */

        public ushort handle; /* attribute handle */

        public ushort nPairs; /* number of write unit pairs */
        public IntPtr valuePairs; /* write unit pairs */
    }

    public struct CSR_BLE_MSG_NOTIFY
    {
        /**
         *  Result supplier, <b>0</b> means success.
         */
        public ushort supplier;
        public ushort result;
    }
}