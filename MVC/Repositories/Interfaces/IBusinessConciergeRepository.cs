using Repositories.DataModels;

namespace Repositories.Interfaces
{
    public interface IBusinessConciergeRepository
    {
        Task<int> addBusiness(Business business);

        Task<int> addConcierge(Concierge concierge);

        Task<int> addRequestConcierge(RequestConcierge requestConcierge);

        Task<int> addRequestBusiness(RequestBusiness requestBusiness);
    }
}
