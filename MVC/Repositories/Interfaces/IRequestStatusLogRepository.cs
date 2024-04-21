using Repositories.DataModels;

namespace Repositories.Interfaces
{
    public interface IRequestStatusLogRepository
    {
        List<RequestStatusLog> GetRequestStatusLogByRequestId(int requestId);

        Task<bool> addRequestSatatusLog(RequestStatusLog requestStatusLog);

        Task<bool> updateRequestSatatusLog(RequestStatusLog requestStatusLog);

        Task<bool> addBlockRequest(BlockRequest blockRequest);
    }
}
