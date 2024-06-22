using Microsoft.Azure.Cosmos;
using dog_adopter.Models;
// using System.Text.Json;
// using System.Text.Json.Serialization;

namespace dog_adopter.Data
{
    public class CosmosSQLDatabase : IDatabaseAdapter
    {
        public CosmosClient _cosmosClient;
        private Database _database;
        private Container _container;

        public CosmosSQLDatabase()
        {
            string cosmos_conn = Environment.GetEnvironmentVariable("COSMOS_CONN");

            // Initialize CosmosClient
            _cosmosClient = new CosmosClient(cosmos_conn);

        }

        

        public async Task InitializeAsync()
        {
            _database = await _cosmosClient.CreateDatabaseIfNotExistsAsync("dog_adopter");
            _container = await _database.CreateContainerIfNotExistsAsync("rescue_dogs", "/breed");
            Console.WriteLine("Cosmos DB and Container initialized successfully.");
        }

        public async Task<RescueDog> GetRescueDog(Breed breed, Guid id)
        {
            try
            {
                ItemResponse<RescueDog> response = await _container.ReadItemAsync<RescueDog>(id.ToString(), new PartitionKey(breed.ToString()));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<List<RescueDog>> GetRescueDogs()
        {
            var query = new QueryDefinition("SELECT * FROM c");
            FeedIterator<RescueDog> resultSet = _container.GetItemQueryIterator<RescueDog>(query);

            List<RescueDog> results = new List<RescueDog>();
            
            while (resultSet.HasMoreResults)
            {
                FeedResponse<RescueDog> response = await resultSet.ReadNextAsync();
                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task<bool> CreateRescueDog(RescueDog rescueDog)
        {
            try
            {
                ItemResponse<RescueDog> response = await _container.CreateItemAsync<RescueDog>(rescueDog);

                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to create rescue dog.");
                throw;
                return false;
            }
        }

        public async Task<bool> UpdateRescueDog(RescueDog rescueDog)
        {
            try
            {
                ItemResponse<RescueDog> response = await _container.UpsertItemAsync<RescueDog>(rescueDog);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}