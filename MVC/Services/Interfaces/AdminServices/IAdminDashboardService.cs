using Microsoft.AspNetCore.Http;
using Services.ViewModels.Admin;
using Services.ViewModels.Physician;
using System.Data;

namespace Services.Interfaces.AdminServices
{
    public interface IAdminDashboardService
    {
        AdminDashboard getallRequests();

        TableModel GetNewRequest(String status, int pageNo, String patientName, int regionId, int requesterTypeId);

        Dictionary<int, String> getPhysiciansByRegion(int regionId);

        Tuple<String, String, int> getRequestClientEmailAndMobile(int requestId);

        Agreement getUserDetails(String token);

        DataTable exportAllData();

        DataTable exportData(String status, int pageNo, String patientName, int regionId, int requesterTypeId);

        bool SendRequestLink(SendLink model,HttpContext httpContext);

        Task<bool> createRequest(CreateRequest model, int aspNetUserId, bool isAdmin);

        bool RequestSupport(RequestSupport model);

        EncounterForm getEncounterDetails(int requestId, bool isAdmin);

        Task<bool> updateEncounter(EncounterForm model, int requestId, int aspNetUserId);

        ViewCase getRequestDetails(int requestId, bool isAdmin);

        Task<bool> updateRequest(ViewCase model);

        ConcludeCare getConcludeCare(int requestId);
    }
}
