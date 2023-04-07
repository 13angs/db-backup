using backend.Interfaces;

namespace backend.Services
{
    public class BackupService : IBackup
    {
        private readonly IMongoBackup _mongoBackup;
        public BackupService(IMongoBackup mongoBackup)
        {
            _mongoBackup = mongoBackup;
        }
        public async Task RunBackup()
        {
            await _mongoBackup.RunBackup();
        }
    }
}