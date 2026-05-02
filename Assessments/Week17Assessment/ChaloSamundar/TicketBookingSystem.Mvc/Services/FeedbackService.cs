using MongoDB.Driver;
using TicketBookingSystem.Mvc.ViewModels;

namespace TicketBookingSystem.Mvc.Services;

public class FeedbackService
{
    private readonly IMongoCollection<FeedbackViewmodel> _collection;

        public FeedbackService(IConfiguration config)
        {
            var settings = config.GetSection("MongoDbSettings").Get<MongoDbSettings>();

            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("FeedbackDB");

            _collection = database.GetCollection<FeedbackViewmodel>("Feedback");
        }

        public async Task CreateAsync(FeedbackViewmodel feedback)
        {
            await _collection.InsertOneAsync(feedback);
}

    internal class MongoDbSettings
    {
        public MongoClientSettings ConnectionString { get; internal set; } = null!;
        public string DatabaseName { get; internal set; } = null!;
        public string FeedbackCollection { get; set; } = null!;
    }
}
