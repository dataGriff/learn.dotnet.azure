# Basic Console App to Consume Change Feed


```bash
func init Replicate --worker-runtime dotnet-isolated --target-framework net8.0
cd Replicate
func new --name ReplicateCartFunction --template "ComosDBTrigger"
```

* local.settings.json

```json
{
    "IsEncrypted": false,
    "Values": {
        "AzureWebJobsStorage": "UseDevelopmentStorage=true",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
        "CosmosDBConnectionString": "AccountEndpoint={Insert Here}"
    }
}
```

```bash
func start
```