using Services.ViewModels;

namespace Services.Interfaces.PatientServices
{
    public interface IPatientDashboardService
    {
        List<Dashboard> GetUsersMedicalData(int aspNetUserId);
    }
}
