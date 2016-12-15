using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;

namespace SendD2C
{
    public class CloudSender
    {
        private DeviceClient _deviceClient;
        private string _iotHubUri = "SimpleWeatherHub.azure-devices.net";
        private string _deviceName = "myRaspi";
        private string _deviceKey = "douBC/ePxlZHHLYs8vMkqvUOlSgr1GvH1qxYf0zUeWU=";

        public CloudSender()
        {
            _deviceClient = DeviceClient.Create(_iotHubUri,
                new DeviceAuthenticationWithRegistrySymmetricKey(_deviceName, _deviceKey));
        }

        public async Task SendWeatherData()
        {
            var random = new Random();

            while (true)
            {
                var currentTemperature = 20 + random.NextDouble()*10 - 5;
                var weatherData = new
                {
                    deviceId = "myRaspi",
                    temperature = currentTemperature,
                    latitude = 50.276800f,
                    longitude = 19.181479f
                };
                await SendDeviceToCloudMessagesAsync(weatherData);
            }
        }

        private async Task SendDeviceToCloudMessagesAsync(object data)
        {
            var messageString = JsonConvert.SerializeObject(data);
            var message = new Message(Encoding.ASCII.GetBytes(messageString));
            await _deviceClient.SendEventAsync(message);
            Task.Delay(1000).Wait();
        }
    }
}
