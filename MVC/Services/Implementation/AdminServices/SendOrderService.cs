using Repositories.DataModels;
using Repositories.Interfaces;
using Services.Interfaces.AdminServices;
using Services.ViewModels.Admin;

namespace Services.Implementation.AdminServices
{
    public class SendOrderService : ISendOrderService
    {
        private readonly IHealthProfessionalRepository _healthProfessionalRepository;

        public SendOrderService(IHealthProfessionalRepository healthProfessionalRepository)
        {
            _healthProfessionalRepository = healthProfessionalRepository;
        }

        public SendOrder getSendOrderDetails(int requestId)
        { 
            return new SendOrder()
            {
                Professions = _healthProfessionalRepository.getHealthProfessionalTypes()
                                                    .ToDictionary(healthProfessionalType => healthProfessionalType.HealthProfessionalId,
                                                                  healthProfessionalType => healthProfessionalType.ProfessionName),
            };
        }

        public HealthProfessional getBussinessData(int venderId)
        {
            return _healthProfessionalRepository.getHealthProfessional(venderId);
        }

        public async Task<bool> addOrderDetails(SendOrder model,int requestId)
        {
            return await _healthProfessionalRepository.addOrderDetails(
                new OrderDetail()
                {
                    VendorId = model.SelectedBusiness,
                    RequestId = requestId,
                    FaxNumber = model.FaxNumber,
                    Email = model.Email,
                    BusinessContact = model.Contact,
                    Prescription = model.OrderDetails,
                    NoOfRefill = model.NoOfRefill,
                    CreatedDate = DateTime.Now,
                });
        }

        public Dictionary<int, string> getBussinessByProfession(int professionId)
        {
            return _healthProfessionalRepository.getHealthProfessionalByProfession(professionId)
                                           .ToDictionary(healthProfessional => healthProfessional.VendorId,
                                                         healthProfessional => healthProfessional.VendorName);
        }
    }
}
