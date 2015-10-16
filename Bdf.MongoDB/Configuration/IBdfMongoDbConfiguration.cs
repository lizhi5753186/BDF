namespace Bdf.MongoDb.Configuration
{
    public interface IBdfMongoDbConfiguration
    {
        string ConnectionString { get; set; }

        string DatabaseName { get; set; } 
    }
}