using Microsoft.AspNetCore.Http;
using Services.ViewModels.Admin;
using System.ComponentModel.DataAnnotations;

namespace Services.ViewModels.Physician
{
    public class PhysicianDashboard
    {
        public Dictionary<int,string> Regions { get; set; }

        public TableModel NewRequests { get; set; }

        public int NewRequestCount { get; set; }

        public TableModel PendingRequests { get; set; }

        public int PendingRequestCount { get; set; }

        public TableModel ActiveRequests { get; set; }

        public int ActiveRequestCount { get; set; }

        public TableModel ConcludeRequests { get; set; }

        public int ConcludeRequestCount { get; set; }

        public Agreement SendAgreement { get; set; }

        public SendLink SendLink { get; set; }

        public PhysicianTransferRequest PhysicianTransferRequest { get; set; }
    }

    public class PhysicianScheduling
    {
        public SchedulingTableMonthWise TableData { get; set; }

        public CreateShift CreateShift { get; set; }

    }

    public class ConcludeCare
    {
        public List<FileModel>? FileList { get; set; }

        public IFormFile File { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string Notes { get; set; }

    }

}
