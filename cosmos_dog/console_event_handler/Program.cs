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

namespace ConsoleEventHandler
{
    class Program
    {
        private static CosmosClient _client { get; set; }

                private static readonly CloudEventFormatter formatter = new JsonEventFormatter();

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
                    //.WithStartTime(DateTime.MinValue.ToUniversalTime()) //starts from the beginning of time
                    .Build();

                await RescueDogChanges.StartAsync();
                Console.WriteLine("Change Feed Processor started. Press any key to stop.");
                Console.ReadKey(true);
                await RescueDogChanges.StopAsync();

            }).Wait();
        }




        private static async Task ProcessChanges(IReadOnlyCollection<dynamic> docs, CancellationToken cancellationToken)
        {
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
            var result = json;

             Console.WriteLine(result);
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


