using System;
using Microsoft.Azure.Cosmos;

string cosmos_conn = System.Environment.GetEnvironmentVariable("COSMOS_CONN");
CosmosClient client = new(cosmos_conn);


Database database = await client.CreateDatabaseIfNotExistsAsync("cosmicworks");

Container container = await database.CreateContainerIfNotExistsAsync("products", "/categoryId", 400);

Product new_saddle = new()
{
    id = "706cd7c6-db8b-41f9-aea2-0e0c7e8eb009",
    categoryId = "9603ca6c-9e28-4a02-9194-51cdb7fea816",
    name = "Road Saddle",
    price = 45.99d,
    tags = new string[]
    {
        "tan",
        "new",
        "crisp"
    }
};

string id = "706cd7c6-db8b-41f9-aea2-0e0c7e8eb009";
string categoryId = "9603ca6c-9e28-4a02-9194-51cdb7fea816";
PartitionKey partitionKey = new(categoryId);
Product saddle;

try
{
    saddle = await container.ReadItemAsync<Product>(id, partitionKey);
}
catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
{
    Console.WriteLine("Item not found therefore creating");
    saddle = await container.CreateItemAsync<Product>(new_saddle);
    Console.WriteLine("Item created");
}


saddle.price = 32.55d;
saddle.name = "Road LL Saddle";

await container.UpsertItemAsync<Product>(saddle);


Console.WriteLine("Read saddle..");
Product saddle_read = await container.ReadItemAsync<Product>(id, partitionKey);

Console.WriteLine($"[{saddle_read.id}]\t{saddle_read.name} ({saddle.price:C})");

await container.DeleteItemAsync<Product>(id, partitionKey);