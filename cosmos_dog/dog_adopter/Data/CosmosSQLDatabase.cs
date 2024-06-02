using Microsoft.Azure.Cosmos;

namespace dog_adopter.Data
{
    public class CosmosSQLDatabase 
    {
        public CosmosClient _cosmosClient;
        private Database _database;
        private Container _container;

        public void CosmosInit()
        {
            string cosmos_conn = Environment.GetEnvironmentVariable("COSMOS_CONN");

            // Initialize CosmosClient
            _cosmosClient = new CosmosClient(cosmos_conn);

            // Call the async initialization method.
            InitializeAsync().GetAwaiter().GetResult();
        }

        private async Task InitializeAsync()
        {
            _database = await _cosmosClient.CreateDatabaseIfNotExistsAsync("dog_adopter");
            _container = await _database.CreateContainerIfNotExistsAsync("rescue_dogs", "/breed");
        }
    }
}