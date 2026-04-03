using DecentralizedInventoryManagementSystem.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DecentralizedInventoryManagementSystem.Services
{
    public class LaptopService : ILaptopService
    {
        private readonly IMongoCollection<Laptop> _laptopCollection;
        public LaptopService(IOptions<DatabaseSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);

            var database = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);

            _laptopCollection = database.GetCollection<Laptop>(
                dbSettings.Value.CollectionName);
        }

        public async Task CreateLaptopAsync(Laptop laptop) =>
            await _laptopCollection.InsertOneAsync(laptop);

        public async Task<IList<Laptop>> GetLaptopsAsync() =>
            await _laptopCollection.Find(_ => true).ToListAsync();
    }
}