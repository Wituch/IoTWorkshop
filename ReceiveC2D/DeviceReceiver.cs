using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;

namespace ReceiveC2D
{
    public class DeviceReceiver
    {
        private DeviceClient _deviceClient;
        private string _iotHubUri = "SimpleWeatherHub.azure-devices.net";
        private string _deviceName = "myRaspi";
        private string _deviceKey = "douBC/ePxlZHHLYs8vMkqvUOlSgr1GvH1qxYf0zUeWU=";

        public DeviceReceiver()
        {
            _deviceClient = DeviceClient.Create(_iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey(_deviceName, _deviceKey));
        }

       public async Task ReceiveC2D()
        {
            Debug.WriteLine("\nReceiving cloud to device messages from service");
            while (true)
            {
                var receivedMessage = await _deviceClient.ReceiveAsync();
                if (receivedMessage == null) continue;

                Debug.WriteLine("Received message: {0}", Encoding.ASCII.GetString(receivedMessage.GetBytes()));

                await _deviceClient.CompleteAsync(receivedMessage);
            }
        }
    }
}
