using Repositories.DataModels;

namespace Repositories.Interfaces
{
    public interface IRequestRepository
    {
        Task<int> addRequest(Request request);

        Request getRequestByRequestId(int requestId);

        Task<bool> updateRequest(Request request);
    }
}
