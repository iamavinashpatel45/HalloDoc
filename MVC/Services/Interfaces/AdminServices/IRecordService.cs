using Services.ViewModels.Admin;
using System.Data;

namespace Services.Interfaces.AdminServices
{
    public interface IRecordService
    {
        Records getRecords(Records model);

        EmailSmsLogs getEmailLog(EmailSmsLogs model);

        List<EmailSmsLogTableData> getEmailLogTabledata(EmailSmsLogs model);

        EmailSmsLogs getSMSlLog(EmailSmsLogs model);

        List<EmailSmsLogTableData> getSMSLogTabledata(EmailSmsLogs model);

        DataTable exportAllRecords();   

        PatientHistory getPatientHistory(PatientHistory model,int pageNo);

        PatientHistoryTable getPatientHistoryTable(string data, int pageNo);

        PatientRecord getPatientRecord(int userId, int pageNo);

        BlockHistory getBlockHistory(BlockHistory model, int pageNo);

        BlockHistoryTable getBlockHistoryTable(string data, int pageNo, string Date);

        Task<bool> ubblockRequest(int requestId);
    }
}
