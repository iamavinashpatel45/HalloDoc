using Repositories.DataModels;
using Services.ViewModels.Admin;

namespace Services.Interfaces.AdminServices
{
    public interface ISendOrderService
    {
        SendOrder getSendOrderDetails(int requestId);

        HealthProfessional getBussinessData(int venderId);

        Task<bool> addOrderDetails(SendOrder model, int requestId); 

        Dictionary<int, string> getBussinessByProfession(int professionId);
    }
}
