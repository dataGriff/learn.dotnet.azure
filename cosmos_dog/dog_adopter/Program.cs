using System;
using Microsoft.Azure.Cosmos;
using dog_adopter.Models;
using System.Threading;

RescueDog rescueDog = new RescueDog("Phil", Breed.Beagle, Status.Available);

Console.WriteLine($"Created rescue dog: {rescueDog.Name} ({rescueDog.Breed})");
Console.WriteLine($"The rescue dog has an id of {rescueDog.Id} and was created on {rescueDog.Timestamp}");

// string cosmos_conn = System.Environment.GetEnvironmentVariable("COSMOS_CONN");
// CosmosClient client = new(cosmos_conn, new CosmosClientOptions()
// {
//     SerializerOptions = new CosmosSerializationOptions()
//     {
//         PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase,
//     }
// });

// Database database = await client.CreateDatabaseIfNotExistsAsync("dog_adopter");

// Container container = await database.CreateContainerIfNotExistsAsync("rescue_dogs", "/breed");

// await database.CreateContainerIfNotExistsAsync("lease", "/id");

// RescueDog rescue_dog1 = new()
// {
//     Id = Guid.NewGuid(),
//     Name = "Phil",
//     Breed = "GoldenRetriever",
//     Status = "Available",
//     Timestamp = DateTime.UtcNow
// };

// RescueDog rescue_dog_created = await container.CreateItemAsync<RescueDog>(rescue_dog1);
// Console.WriteLine($"Created rescue dog: {rescue_dog_created.Name} ({rescue_dog_created.Breed})");

// rescue_dog1.Status = "Adopted";

// Console.ReadKey(true);

// RescueDog rescue_dog_updated = await container.UpsertItemAsync<RescueDog>(rescue_dog1);
// Console.WriteLine($"Updated rescue dog: {rescue_dog_created.Name} ({rescue_dog_created.Status})");

