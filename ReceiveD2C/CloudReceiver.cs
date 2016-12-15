using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace ReceiveD2C
{
    public class CloudReceiver
    {
        private string _connectionString = "HostName=SimpleWeatherHub.azure-devices.net;SharedAccessKeyName=service;SharedAccessKey=5fhYg1aodzXETRDX3cr6lVU6hoASsOLIKv+VErQ4AVo=";
        private string _iotHubD2cEndpoint = "messages/events";
        EventHubClient _eventHubClient;

        public async Task ReceiveWeatherData()
        {
            Console.WriteLine("Start receiving weather data:");
            _eventHubClient = EventHubClient.CreateFromConnectionString(_connectionString, _iotHubD2cEndpoint);
            var d2cPartitions = _eventHubClient.GetRuntimeInformation().PartitionIds;

            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
                Console.WriteLine("Exiting...");
            };

            var tasks = new List<Task>();
            foreach (var partition in d2cPartitions)
            {
                tasks.Add(ReceiveMessagesFromDeviceAsync(partition, cts.Token));
            }
            Task.WaitAll(tasks.ToArray());
        }

        private async Task ReceiveMessagesFromDeviceAsync(string partition, CancellationToken ct)
        {
            var eventHubReceiver = _eventHubClient.GetDefaultConsumerGroup().CreateReceiver(partition, DateTime.UtcNow);
            while (true)
            {
                if (ct.IsCancellationRequested) break;
                var eventData = await eventHubReceiver.ReceiveAsync();
                if (eventData == null) continue;

                var data = Encoding.UTF8.GetString(eventData.GetBytes());
                Console.WriteLine("Message received. Partition: {0} Data: '{1}'", partition, data);
            }
        }

    }
}
