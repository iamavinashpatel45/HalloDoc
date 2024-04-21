using System.ComponentModel.DataAnnotations;

namespace Services.ViewModels.Admin
{
    public class AssignAndTransferPopUp
    {
        [Display(Name = "Region")]
        public int SelectedRegion { get; set; }

        [Display(Name = "Physician")]
        public int SelectedPhysician { get; set; }

        public Dictionary<int, String>? Regions { get; set; }

        public int RequestId { get; set; }

        public String? AdminTransferNotes { get; set; }
    }

    public class BlockPopUp
    {
        public int RequestId { get; set; }

        public String? AdminTransferNotes { get; set; }
    }

    public class PhysicianTransferRequest
    {
        public int RequestId { get; set; }

        public String? TransferNotes { get; set; }
    }

    public class CancelPopUp
    {
        public int Reason { get; set; }

        public Dictionary<int, String>? Reasons { get; set; }

        public string? PatientName { get; set; }

        public int RequestId { get; set; }

        public String? AdminTransferNotes { get; set; }
    }

    public class SendLink
    {
        [RegularExpression(@"^[a-zA-Z]+(?: [a-zA-Z]+)*$", ErrorMessage = "The FirstName is not valid")]
        public string FirstName { get; set; }

        [RegularExpression(@"^[a-zA-Z]+(?: [a-zA-Z]+)*$", ErrorMessage = "The LastName is not valid")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "The Email is not valid")]
        public string Email { get; set; }

        [RegularExpression(@"^(?:\+?91)?\s*([1-9]\d{4})\s*(\d{5})$", ErrorMessage = "The Mobile is not valid")]
        public string Mobile { get; set; }
    }

    public class RequestSupport
    {
        public string Message { get; set; }
    }

    public class Agreement
    {
        public bool IsValid { get; set; } = false;

        public string? Message { get; set; }

        public int RequestId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? CancelationReson { get; set; }

        public string Number { get; set; }

        public string Email { get; set; }
    }
}
