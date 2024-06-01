using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CfpLibraryHost
{
    class Program
    {
        private static CosmosClient _client { get; set; }

        static void Main(string[] args)
        {

            Task.Run(async () =>
            {
                _client = new CosmosClient("");

                var _database = _client.GetDatabase("acme-webstore");
                var cartContainer = _database.GetContainer("cart");
                var leaseContainer = _database.GetContainer("lease");

                var cfp = cartContainer.GetChangeFeedProcessorBuilder<dynamic>("CfpLibraryDemo", ProcessChanges)
                    .WithInstanceName("Change Feed Process Library Demo")
                    .WithLeaseContainer(leaseContainer)
                    .WithStartTime(DateTime.MinValue.ToUniversalTime()) //starts from the beginning of time
                    .Build();

                await cfp.StartAsync();
                Console.WriteLine("Change Feed Processor started. Press any key to stop.");
                Console.ReadKey(true);
                await cfp.StopAsync();

            }).Wait();
        }




        private static async Task ProcessChanges(IReadOnlyCollection<dynamic> docs, CancellationToken cancellationToken)
        {
            foreach (var doc in docs)
            {
                Console.WriteLine($"Processing document with id {doc.id}");
                Console.WriteLine(doc.ToString());
            }
        }
    }
}


