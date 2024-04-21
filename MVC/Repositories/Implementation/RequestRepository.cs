using Repositories.DataContext;
using Repositories.DataModels;
using Repositories.Interfaces;

namespace Repositories.Implementation
{
    public class RequestRepository : IRequestRepository
    {
        private readonly HalloDocDbContext _dbContext;

        public RequestRepository(HalloDocDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> addRequest(Request request)
        {
            _dbContext.Requests.Add(request);
            await _dbContext.SaveChangesAsync();
            return request?.RequestId ?? 0;
        }

        public Request getRequestByRequestId(int requestId)
        {
            return _dbContext.Requests.FirstOrDefault(a => a.RequestId==requestId);
        }

        public async Task<bool> updateRequest(Request request)
        {
            _dbContext.Requests.Update(request);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
