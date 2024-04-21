using Repositories.DataModels;

namespace Repositories.Interfaces
{
    public interface ILogsService
    {
        Task<bool> addEmailLog(EmailLog emailLog);

        List<EmailLog> getAllEmailLogs(Func<EmailLog, bool> predicate);

        List<Smslog> getAllSMSLogs(Func<Smslog, bool> predicate);
    }
}
