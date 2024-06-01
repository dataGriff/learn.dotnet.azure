using System;
using System.Linq;
using Microsoft.Azure.Cosmos;

string cosmos_conn = System.Environment.GetEnvironmentVariable("COSMOS_CONN");
CosmosClient client = new(cosmos_conn);

AccountProperties account = await client.ReadAccountAsync();
Console.WriteLine($"Account Name:\t{account.Id}");
Console.WriteLine($"Primary Region:\t{account.WritableRegions.FirstOrDefault()?.Name}");
