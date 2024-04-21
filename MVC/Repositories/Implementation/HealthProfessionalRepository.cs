using Microsoft.EntityFrameworkCore;
using Repositories.DataContext;
using Repositories.DataModels;
using Repositories.Interfaces;
using System.Collections;

namespace Repositories.Implementation
{
    public class HealthProfessionalRepository : IHealthProfessionalRepository
    {
        private readonly HalloDocDbContext _dbContext;

        public HealthProfessionalRepository(HalloDocDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> addHealthProfessional(HealthProfessional healthProfessional)
        {
            _dbContext.HealthProfessionals.Add(healthProfessional);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> updateHealthProfessional(HealthProfessional healthProfessional)
        {
            _dbContext.HealthProfessionals.Update(healthProfessional);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public List<HealthProfessionalType> getHealthProfessionalTypes()
        {
            return _dbContext.HealthProfessionalTypes.ToList();
        }

        public List<HealthProfessional> getHealthProfessionalByProfessionWithType(int professionId,String searchElement)
        {
            return _dbContext.HealthProfessionals.Include(a => a.ProfessionNavigation)
                               .Where(a => (professionId == 0 || a.Profession == professionId) && a.IsDeleted == new BitArray(1, false) &&
                                     (searchElement == null || a.VendorName.ToLower().Contains(searchElement.ToLower()))).ToList();
        }

        public List<HealthProfessional> getHealthProfessionalByProfession(int professionId)
        {
            return _dbContext.HealthProfessionals.Where(a => a.Profession == professionId).ToList();
        }

        public HealthProfessional getHealthProfessional(int VenderId)
        {
            return _dbContext.HealthProfessionals.FirstOrDefault(a => a.VendorId == VenderId);
        }

        public async Task<bool> addOrderDetails(OrderDetail orderDetail)
        {
            _dbContext.OrderDetails.Add(orderDetail);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
