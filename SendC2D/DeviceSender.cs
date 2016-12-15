using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;

namespace SendC2D
{
    public class DeviceSender
    {
        private ServiceClient _serviceClient;
        private string _connectionString = "HostName=SimpleWeatherHub.azure-devices.net;SharedAccessKeyName=service;SharedAccessKey=5fhYg1aodzXETRDX3cr6lVU6hoASsOLIKv+VErQ4AVo=";

        public void SendMessage()
        {
            Console.WriteLine("Send Cloud-to-Device message\n");
            _serviceClient = ServiceClient.CreateFromConnectionString(_connectionString);
            Console.WriteLine("Press any key to send a C2D message.");
            Console.ReadLine();
            SendCloudToDeviceMessageAsync("Cloud to device message").Wait();
            Console.ReadLine();
        }

        private async Task SendCloudToDeviceMessageAsync(string message)
        {
            var commandMessage = new Message(Encoding.ASCII.GetBytes(message));
            await _serviceClient.SendAsync("myRaspi", commandMessage);
        }
    }
}
