using System;
using Microsoft.Azure.Cosmos;

string cosmos_conn = System.Environment.GetEnvironmentVariable("COSMOS_CONN");
CosmosClient client = new(cosmos_conn);


Database database = await client.CreateDatabaseIfNotExistsAsync("cosmicworks");
Container container = await database.CreateContainerIfNotExistsAsync("products", "/categoryId", 400);
    

Product saddle = new("0120", "Worn Saddle", "9603ca6c-9e28-4a02-9194-51cdb7fea816");
Product handlebar = new("012A", "Rusty Handlebar", "9603ca6c-9e28-4a02-9194-51cdb7fea816");

PartitionKey partitionKey = new ("9603ca6c-9e28-4a02-9194-51cdb7fea816");

TransactionalBatch batch = container.CreateTransactionalBatch(partitionKey)
    .CreateItem<Product>(saddle)
    .CreateItem<Product>(handlebar);

using TransactionalBatchResponse response = await batch.ExecuteAsync();

Console.WriteLine($"Status:\t{response.StatusCode}");