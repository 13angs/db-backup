using backend.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace backend.Services
{
    public class MongoBackupService : IMongoBackup
    {
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _env;
        public MongoBackupService(IConfiguration configuration, IHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }
        public async Task RunBackup()
        {
            string conStr = _configuration["Mongodb:Sample:ConnectionString"];
            IMongoClient client = new MongoClient(conStr);
            string dbName = _configuration["Mongodb:Sample:Database"];
            IMongoDatabase database = client.GetDatabase(dbName);
            string backupPath = Path.Combine(_env.ContentRootPath, _configuration["Static:Name"], "backup.bson");

            var backupOptions = new BsonDocument
            {
                { "out", backupPath },
                { "gzip", true }
            };

            var command = new BsonDocumentCommand<BsonDocument>(new BsonDocument
            {
                { "mongodump", 1 },
                { "--db", dbName },
                { "--archive", "" },
                { "--gzip", "" }
            }.AddRange(backupOptions));

            var result = await database.RunCommandAsync(command);
        }
    }
}