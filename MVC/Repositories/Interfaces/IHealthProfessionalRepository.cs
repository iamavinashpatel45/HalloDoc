using Repositories.DataModels;

namespace Repositories.Interfaces
{
    public interface IHealthProfessionalRepository
    {
        Task<bool> addHealthProfessional(HealthProfessional healthProfessional);

        Task<bool> updateHealthProfessional(HealthProfessional healthProfessional);

        List<HealthProfessionalType> getHealthProfessionalTypes();

        List<HealthProfessional> getHealthProfessionalByProfessionWithType(int professionId, String searchElement);

        List<HealthProfessional> getHealthProfessionalByProfession(int professionId);

        HealthProfessional getHealthProfessional(int VenderId);

        Task<bool> addOrderDetails(OrderDetail orderDetail);
    }
}
