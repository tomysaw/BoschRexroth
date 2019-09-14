using Bosch.VideoSDK;
using Bosch.VideoSDK.CameoLib;
using Bosch.VideoSDK.Device;
using Bosch.VideoSDK.MediaDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoschRexroth.Root
{
    public class VideoService
    {
        private DeviceConnector m_deviceConnector = new DeviceConnectorClass();
        private DeviceProxy m_deviceProxy = null;
        private DataStream _stream;
        private Cameo m_cameo = null;

        #region API

        public void Initialize()
        {
            m_deviceConnector.ConnectResult += new Bosch.VideoSDK.GCALib._IDeviceConnectorEvents_ConnectResultEventHandler(DeviceConnector_ConnectResult);

            m_deviceConnector.ConnectAsync("https://hackathon:!Hackath0n@192.168.0.2/onvif/device_service", "GCA.ONVIF.DeviceProxy");
        }

        public byte[] GetFrame()
        {
            Console.WriteLine("Stream {0}", _stream != null);

            return null;
        }

        #endregion

        #region Private

        private void DeviceConnector_ConnectResult(ConnectResultEnum connectResult, string url, DeviceProxy deviceProxy)
        {
            bool success = false;

            if (connectResult == ConnectResultEnum.creInitialized)
            {
                if (deviceProxy.VideoInputs.Count > 0)
                {
                    success = true;

                    try
                    {
                        _stream = deviceProxy.VideoInputs[1].Stream;
                        m_cameo.SetVideoStream(_stream);
                    }
                    catch (Exception ex)
                    {
                        success = false;
                    }
                }
            }

            if (success)
            {
                m_deviceProxy = deviceProxy;
                m_deviceProxy.ConnectionStateChanged += new Bosch.VideoSDK.GCALib._IDeviceProxyEvents_ConnectionStateChangedEventHandler(DeviceProxy_ConnectionStateChanged);
            }
            else
            {
                if (deviceProxy != null)
                    deviceProxy.Disconnect();
            }

        }

        private void DeviceProxy_ConnectionStateChanged(object eventSource, ConnectResultEnum state)
        {
            if (state == ConnectResultEnum.creConnectionTerminated)
            {
                _stream = null;
                m_deviceProxy.ConnectionStateChanged -= new Bosch.VideoSDK.GCALib._IDeviceProxyEvents_ConnectionStateChangedEventHandler(DeviceProxy_ConnectionStateChanged);
                m_deviceProxy = null;
            }
        }

        #endregion
    }
}
