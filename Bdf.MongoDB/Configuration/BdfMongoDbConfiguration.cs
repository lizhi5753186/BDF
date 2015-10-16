namespace Bdf.MongoDb.Configuration
{
    internal class BdfMongoDbConfiguration : IBdfMongoDbConfiguration
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}