using backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/v1/backups")]
    public class BackupController : ControllerBase
    {
        private readonly IBackup _backup;
        public BackupController(IBackup backup)
        {
            _backup = backup;
        }

        [HttpPost]
        public async Task <ActionResult> Backup()
        {
            await _backup.RunBackup();
            return Ok();
        }
    }
}