namespace Services.ViewModels.Admin
{
    public class AdminDashboard
    {
        public TableModel NewRequests { get; set; }

        public int NewRequestCount { get; set; }

        public TableModel PendingRequests { get; set; }

        public int PendingRequestCount { get; set; }

        public TableModel ActiveRequests { get; set; }

        public int ActiveRequestCount { get; set; }

        public TableModel ConcludeRequests { get; set; }

        public int ConcludeRequestCount { get; set; }

        public TableModel TocloseRequests { get; set; }

        public int TocloseRequestCount { get; set; }

        public TableModel UnpaidRequests { get; set; }

        public int UnpaidRequestCount { get; set; }

        public CancelPopUp CancelPopup { get; set; }

        public Agreement SendAgreement { get; set; }

        public AssignAndTransferPopUp AssignAndTransferPopup { get; set; }

        public BlockPopUp BlockPopup { get; set; }

        public SendLink SendLink { get; set; }
    }

    public class TableModel
    {
        public List<TablesData> TableDatas { get; set; }

        public int TotalRequests { get; set; }

        public int PageNo { get; set; }

        public bool IsFirstPage { get; set; }

        public bool IsLastPage { get; set; }

        public bool IsNextPage { get; set; }

        public bool IsPreviousPage { get; set; }

        public int StartRange { get; set; }

        public int EndRange { get; set; }
    }

    public class TablesData
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Notes { get; set; }

        public string PhysicianName { get; set; }

        public int RequesterType { get; set; }

        public int? AssignPhysician { get; set; }

        public int Requester { get; set; }

        public int RequestId { get; set; }

        public int? RegionId { get; set; }

        public string RequesterFirstName { get; set; }

        public string? RequesterLastName { get; set; }

        public string Email { get; set; }

        public string? Mobile { get; set; }

        public int IsEncounter { get; set; }

        public short? EncounterType { get; set; }

        public int Isfinalize { get; set; }

        public string? RequesterMobile { get; set; }

        public string BirthDate { get; set; }

        public DateTime? DateOfService { get; set; }

        public string RequestdDate { get; set; }

        public string? Street { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? ZipCode { get; set; }
    }
}
