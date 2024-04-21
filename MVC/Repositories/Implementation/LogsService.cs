using Microsoft.EntityFrameworkCore;
using Repositories.DataContext;
using Repositories.DataModels;
using Repositories.Interfaces;

namespace Repositories.Implementation
{
    public class LogsService : ILogsService
    {

        private readonly HalloDocDbContext _dbContext;

        public LogsService(HalloDocDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> addEmailLog(EmailLog emailLog)
        {
            _dbContext.EmailLogs.Add(emailLog);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public List<EmailLog> getAllEmailLogs(Func<EmailLog, bool> predicate)
        {
            return _dbContext.EmailLogs.Include(a => a.Role).Where(predicate).ToList();
        }

        public List<Smslog> getAllSMSLogs(Func<Smslog, bool> predicate)
        {
            return _dbContext.Smslogs.Include(a => a.Role).Where(predicate).ToList();
        }
    }
}
