using System;
using System.Linq;
using Microsoft.Azure.Cosmos;

string endpoint = System.Environment.GetEnvironmentVariable("COSMOS_ENDPOINT");
string key = System.Environment.GetEnvironmentVariable("COSMOS_KEY");
CosmosClient client = new(endpoint, key);

AccountProperties account = await client.ReadAccountAsync();
Console.WriteLine($"Account Name:\t{account.Id}");
Console.WriteLine($"Primary Region:\t{account.WritableRegions.FirstOrDefault()?.Name}");
