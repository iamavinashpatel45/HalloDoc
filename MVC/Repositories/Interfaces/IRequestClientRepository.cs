using Repositories.DataModels;

namespace Repositories.Interfaces
{
    public interface IRequestClientRepository
    {
        List<Region> getAllRegions();

        List<CaseTag> getAllReason();

        List<RequestClient> getAllRequestClients();

        List<RequestClient> getRequestClientsBasedOnFilter(Func<RequestClient,bool> predicate);

        List<BlockRequest> getRequestClientsAndBlockRequestBasedOnFilter(Func<BlockRequest, bool> predicate);

        int countRequestClientsAndBlockRequestBasedOnFilter(Func<BlockRequest, bool> predicate);

        List<RequestClient> getRequestClientByStatus(Func<RequestClient, bool> predicate, int skip);

        int countRequestClientByStatusForAdmin(List<int> status);

        int countRequestClientByStatusForPhysician(List<int> status, int aspNetUserId);

        int countRequestClientByStatusAndFilter(Func<RequestClient, bool> predicate);

        List<RequestClient> getAllRequestClientForUser(int userId);

        Task<bool> addRequestClient(RequestClient requestClient);

        RequestClient getRequestClientByRequestId(int requestId);

        RequestClient getRequestClientAndRequestByRequestId(int requestId);

        Task<bool> updateRequestClient(RequestClient requestClient);

        List<RequestClient> getAllRequestClientsByUserId(int userId, int skip);
        
        int countRequestClientsByUserId(int userId);

        Task<bool> deleteBlockRequest(int requestId);
    }
}