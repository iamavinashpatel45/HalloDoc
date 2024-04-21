using Repositories.DataModels;

namespace Services.ViewModels.Admin
{
    public class Records
    {
        public int? RequestStatus { get; set; }

        public string? PatientName { get; set; }

        public int? RequestType { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? ProviderName { get; set; }

        public string? Email { get; set;}

        public string? Number { get; set; }

        public List<RecordTableData>? RecordTableDatas { get; set; }
    }

    public class RecordTableData
    {
        public int RequestId { get; set; }

        public string PatientName { get; set; }

        public int RequestType { get; set;}

        public DateTime? DateOfService { get; set; }

        public DateTime ColseCaseDate { get; set;}

        public string Email { get; set;}

        public string Phone { get; set;}

        public string Address { get; set;}

        public string Zip { get; set;}

        public int Status { get; set; }

        public string PhysicianName { get; set; } = "-";

        public string PhysicianNotes { get; set; } = "-";

        public string CancleNotes { get; set; } = "-";

        public string AdminNotes { get; set; } = "-";

        public string PatientNotes { get; set; } = "-";
    }

    public class EmailSmsLogs
    {
        public Dictionary<int , string>? Roles { get; set; }

        public int? Role { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public DateOnly? SendDate { get; set; } 

        public DateOnly? CreatedDate { get; set;} 

        public List<EmailSmsLogTableData>? EmailSmsLogTableDatas { get; set; }
    }

    public class EmailSmsLogTableData
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Action { get; set; }

        public string RoleName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime SendDate { get; set; }

        public DateTime CreatedDate { get; set;}

        public string Send { get; set; }

    }

    public class PatientHistory
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public PatientHistoryTable PatientHistoryTable { get; set; }
    }

    public class PatientHistoryTable
    {
        public List<PatientHistoryTableData> PatientHistoryTableDatas { get; set; }

        public int TotalRequests { get; set; }

        public int PageNo { get; set; }

        public bool IsFirstPage { get; set; }

        public bool IsLastPage { get; set; }

        public bool IsNextPage { get; set; }

        public bool IsPreviousPage { get; set; }

        public int StartRange { get; set; }

        public int EndRange { get; set; }
    }

    public class PatientHistoryTableData
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }
    }

    public class PatientRecord
    {
        public List<PatientRecordTableData> patientRecordTableDatas { get; set; }

        public int TotalRequests { get; set; }

        public int PageNo { get; set; }

        public bool IsFirstPage { get; set; }

        public bool IsLastPage { get; set; }

        public bool IsNextPage { get; set; }

        public bool IsPreviousPage { get; set; }

        public int StartRange { get; set; }

        public int EndRange { get; set; }
    }

    public class PatientRecordTableData
    {
        public int RequestId { get; set; }

        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Conformation { get; set; }

        public string ProviderName { get; set; }

        public DateTime ConcludeedDate { get; set; }

        public int Ststus { get; set; }

        public int CountDocument { get; set; }
    }

    public class BlockHistory
    {
        public string Name { get; set; }

        public DateOnly? Date { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public BlockHistoryTable BlockHistoryTable { get; set; }
    }

    public class BlockHistoryTable
    {
        public List<BlockHistoryTableData> BlockHistoryTableDatas { get; set; }

        public int TotalRequests { get; set; }

        public int PageNo { get; set; }

        public bool IsFirstPage { get; set; }

        public bool IsLastPage { get; set; }

        public bool IsNextPage { get; set; }

        public bool IsPreviousPage { get; set; }

        public int StartRange { get; set; }

        public int EndRange { get; set; }
    }

    public class BlockHistoryTableData
    {
        public int RequestId { get; set; }

        public string Name { get; set; }

        public DateTime CratedDate { get; set; }

        public string Notes { get; set; }
        
        public string Email { get; set; }

        public bool IsActive { get; set; }

        public string Phone { get; set; }
    }
}
