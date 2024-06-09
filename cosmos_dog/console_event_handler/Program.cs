using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using CloudNative.CloudEvents;
using CloudNative.CloudEvents.SystemTextJson;
using System.Text;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using Azure.Messaging.EventHubs.Producer;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using Azure.Storage.Blobs;

namespace ConsoleEventHandler
{
    class Program
    {
        private static CosmosClient _client { get; set; }


        private static readonly CloudEventFormatter formatter = new JsonEventFormatter();

        private static string eh_con = "Endpoint=sb://localhost;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=EMULATOR_DEV_SAS_VALUE;UseDevelopmentEmulator=true;";

        static void Main(string[] args)
        {

            Task.Run(async () =>
            {
                string cosmos_conn = System.Environment.GetEnvironmentVariable("COSMOS_CONN");
                CosmosClient _client = new(cosmos_conn);


                var _database = _client.GetDatabase("dog_adopter");
                var rescueDogContainer = _database.GetContainer("rescue_dogs");
                var leaseContainer = _database.GetContainer("lease");

                var RescueDogChanges = rescueDogContainer.GetChangeFeedProcessorBuilder<dynamic>("ConsoleRescueDogChanges", ProcessChanges)
                    .WithInstanceName("Reads rescue dog state changes")
                    .WithLeaseContainer(leaseContainer)
                    .WithStartTime(DateTime.MinValue.ToUniversalTime()) //starts from the beginning of time
                    .Build();

                await RescueDogChanges.StartAsync();
                Console.WriteLine("Change Feed Processor started. Press any key to stop.");
                Console.ReadKey(true);
                await RescueDogChanges.StopAsync();

            }).Wait();
        }

        private static async Task ProcessChanges(IReadOnlyCollection<dynamic> docs, CancellationToken cancellationToken)
        {

            // Create a batch of events 
            try
            {
                 EventHubProducerClient producerClient = new EventHubProducerClient(eh_con, "dogrescue.cdc.rescuedog.v1");

                using EventDataBatch eventBatch = await producerClient.CreateBatchAsync();

                // Add events to the batch and send it
            }
            catch (TaskCanceledException ex)
            {
                Console.WriteLine("Operation timed out: " + ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Authentication error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }

            foreach (var doc in docs)
            {
                Console.WriteLine("Hello");
                Console.WriteLine($"Rescue dog {doc.id} has changed");
                Console.WriteLine(doc.ToString());
                Console.WriteLine(doc["name"]);
                RescueDog rescueDog = JsonSerializer.Deserialize<RescueDog>(doc.ToString());


                var cloudEvent = new CloudEvent
                {
                    Id = Guid.NewGuid().ToString(),
                    Type = "dogadoption.cdc.rescuedog.v1",
                    Source = new Uri("/dog_adopter", UriKind.Relative),
                    Time = DateTimeOffset.UtcNow,
                    DataContentType = "application/json",
                    Data = rescueDog
                };

                var bytes = formatter.EncodeStructuredModeMessage(cloudEvent, out var contentType);
                string json = Encoding.UTF8.GetString(bytes.Span);

                // if (!eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes(json))))
                // {
                //     // if it is too large for the batch
                //     throw new Exception($"Dog {doc.id} is too large for the batch and cannot be sent.");
                // }

                // try
                // {
                //     // Use the producer client to send the batch of events to the event hub
                //     await producerClient.SendAsync(eventBatch);

                // }
                // finally
                // {
                //     await producerClient.DisposeAsync();
                // }

                Console.WriteLine(json);


            }
        }
    }

    public class RescueDog
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        // [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("breed")]
        public string Breed { get; set; }

        // [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}


