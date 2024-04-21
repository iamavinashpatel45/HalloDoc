using Repositories.DataContext;
using Repositories.DataModels;
using Repositories.Interfaces;

namespace Repositories.Implementation
{
    public class BusinessConciergeRepository : IBusinessConciergeRepository
    {
        private readonly HalloDocDbContext _dbContext;

        public BusinessConciergeRepository(HalloDocDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> addBusiness(Business business)
        {
            _dbContext.Businesses.Add(business);
            await _dbContext.SaveChangesAsync();
            return business?.BusinessId ?? 0;
        }

        public async Task<int> addConcierge(Concierge concierge)
        {
            _dbContext.Concierges.Add(concierge);
            await _dbContext.SaveChangesAsync();
            return concierge?.ConciergeId ?? 0;
        }

        public async Task<int> addRequestConcierge(RequestConcierge requestConcierge)
        {
            _dbContext.RequestConcierges.Add(requestConcierge);
            await _dbContext.SaveChangesAsync();
            return requestConcierge?.ConciergeId ?? 0;
        }

        public async Task<int> addRequestBusiness(RequestBusiness requestBusiness)
        {
            _dbContext.RequestBusinesses.Add(requestBusiness);
            await _dbContext.SaveChangesAsync();
            return requestBusiness?.RequestBusinessId ?? 0;
        }
    }
}
