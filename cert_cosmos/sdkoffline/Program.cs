using System;
using Microsoft.Azure.Cosmos;

string cosmos_conn = System.Environment.GetEnvironmentVariable("COSMOS_CONN");
CosmosClient client = new(cosmos_conn);


Database database = await client.CreateDatabaseIfNotExistsAsync("cosmicworks");
Console.WriteLine($"New Database:\tId: {database.Id}");

Container container = await database.CreateContainerIfNotExistsAsync("products", "/categoryId");
Console.WriteLine($"New Container:\tId: {container.Id}");


var containerResponse = await container.ReadContainerAsync();
ContainerProperties containerProperties = containerResponse;
containerProperties.DefaultTimeToLive = 300; // expire in 5 minutes
var containerResponseWithTTLEnabled = container.ReplaceContainerAsync(containerProperties);
Console.WriteLine($"Container TTL: {containerProperties.DefaultTimeToLive}");
Console.WriteLine($"Container Indexing Mode: {containerProperties.IndexingPolicy.IndexingMode}");
Console.WriteLine($"Container Indexing Automatic: {containerProperties.IndexingPolicy.Automatic}");
foreach (var index in containerProperties.IndexingPolicy.IncludedPaths)
{
    Console.WriteLine($"Container Indexing Included Path: {index.Path}");
}
foreach (var index in containerProperties.IndexingPolicy.ExcludedPaths)
{
    Console.WriteLine($"Container Indexing Excluded Path: {index.Path}");
}
Console.WriteLine($"Container Indexing Mode: {containerProperties.ConflictResolutionPolicy.Mode}");
Console.WriteLine($"Container Indexing Resolution Path: {containerProperties.ConflictResolutionPolicy.ResolutionPath }");
Console.WriteLine($"Container Indexing Resolution Procedure: {containerProperties.ConflictResolutionPolicy.ResolutionProcedure }");
foreach (var key in containerProperties.UniqueKeyPolicy.UniqueKeys)
{
    Console.WriteLine($"Container Indexing Unique Key: {key}");
}