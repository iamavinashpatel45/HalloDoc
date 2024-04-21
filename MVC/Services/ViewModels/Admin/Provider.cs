using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Services.ViewModels.Admin
{
    public class Provider
    {
        public List<ProviderTable> providers { get; set; }

        public Dictionary<int, string> Regions { get; set; }

        public ContactProvider ContactProvider { get; set; }
    }

    public class ProviderTable
    {
        public int providerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool Notification { get; set; }

        public string Role { get; set; }

        public bool OnCallStatus { get; set; }

        public String Status { get; set; }
    }

    public class ContactProvider
    {
        public int providerId { get; set; }

        public string Message { get; set; }

        public bool email { get; set; } = false;

        public bool sms { get; set; } = false;
    }

    public class ProviderLocation
    {
        public decimal? latitude { get; set; }

        public decimal? longitude { get; set; }

        public string ProviderName { get; set; }
    }

    public class CreateProvider
    {
        public bool isUpdate { get; set; }

        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z]+(?: [a-zA-Z]+)*$", ErrorMessage = "The FirstName is not valid")]
        public string FirstName { get; set; }

        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z]+(?: [a-zA-Z]+)*$", ErrorMessage = "The LastName is not valid")]
        public string LastName { get; set; }

        public string Password { get; set; }

        [Required(ErrorMessage = "The Role field is required.")]
        public int SelectedRole { get; set; }

        public Dictionary<int, string>? Roles { get; set; }

        public string UserName { get; set; }

        [StringLength(50)]
        [DataType(DataType.EmailAddress, ErrorMessage = "The Email is not valid")]
        public string Email { get; set; }

        [StringLength(20)]
        [RegularExpression(@"^(?:\+?91)?\s*([1-9]\d{4})\s*(\d{5})$", ErrorMessage = "The Phone is not valid")]
        public string Phone { get; set; }

        [StringLength(20)]
        [Display(Name = "Mobile")]
        [RegularExpression(@"^(?:\+?91)?\s*([1-9]\d{4})\s*(\d{5})$", ErrorMessage = "The Mobile is not valid")]
        public string Phone2 { get; set; }

        [StringLength(500)]
        public string MedicalLicance { get; set; }

        [StringLength(500)]
        public string NpiNumber { get; set; }

        [StringLength(500)]
        public string Add1 { get; set; }

        [StringLength(500)]
        public string Add2 { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(10)]
        public string Zip { get; set; }

        [Required(ErrorMessage = "The State field is required.")]
        public string SelectedRegion { get; set; }

        [Required(ErrorMessage = "The Region field is required.")]
        public List<string> SelectedRegions { get; set; }

        public Dictionary<int, string>? Regions { get; set; }

        [StringLength(100)]
        public string BusinessName { get; set; }

        [StringLength(200)]
        public string BusinessWebsite { get; set; }

        public IFormFile Photo { get; set; }

        [StringLength(500)]
        public string AdminNotes { get; set; }

        public bool IsAgreementDoc { get; set; }

        [RequiredIfBoolIsTrue(nameof(IsAgreementDoc))]
        public IFormFile? AgreementDoc { get; set; }

        public bool IsBackgroundDoc { get; set; }

        [RequiredIfBoolIsTrue(nameof(IsBackgroundDoc))]
        public IFormFile? BackgroundDoc { get; set; }

        public bool IsHIPAACompliance { get; set; }

        [RequiredIfBoolIsTrue(nameof(IsHIPAACompliance))]
        public IFormFile? HIPAACompliance { get; set; }

        public bool IsNonDisclosureDoc { get; set; }

        [RequiredIfBoolIsTrue(nameof(IsNonDisclosureDoc))]
        public IFormFile? NonDisclosureDoc { get; set; }

    }

    public class EditProvider
    {
        public bool isUpdate { get; set; }

        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z]+(?: [a-zA-Z]+)*$", ErrorMessage = "The FirstName is not valid")]
        public string FirstName { get; set; }

        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z]+(?: [a-zA-Z]+)*$", ErrorMessage = "The LastName is not valid")]
        public string LastName { get; set; }

        public string Password { get; set; }

        [Required(ErrorMessage = "The Role field is required.")]
        public int SelectedRole { get; set; }

        public int Status { get; set; }

        public Dictionary<int, string>? Roles { get; set; }

        public string UserName { get; set; }

        [StringLength(50)]
        [DataType(DataType.EmailAddress, ErrorMessage = "The Email is not valid")]
        public string Email { get; set; }

        [StringLength(20)]
        [RegularExpression(@"^(?:\+?91)?\s*([1-9]\d{4})\s*(\d{5})$", ErrorMessage = "The Phone is not valid")]
        public string Phone { get; set; }

        [StringLength(20)]
        [Display(Name = "Mobile")]
        [RegularExpression(@"^(?:\+?91)?\s*([1-9]\d{4})\s*(\d{5})$", ErrorMessage = "The Mobile is not valid")]
        public string Phone2 { get; set; }

        [StringLength(50)]
        [DataType(DataType.EmailAddress, ErrorMessage = "The SynchronizationEmail is not valid")]
        public string SynchronizationEmail { get; set; }

        [StringLength(500)]
        public string MedicalLicance { get; set; }

        [StringLength(500)]
        public string NpiNumber { get; set; }

        [StringLength(500)]
        public string Add1 { get; set; }

        [StringLength(500)]
        public string Add2 { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(10)]
        public string Zip { get; set; }

        [Required(ErrorMessage = "The State field is required.")]
        public int SelectedRegion { get; set; }

        [Required(ErrorMessage = "The Region field is required.")]
        public List<int> SelectedRegions { get; set; }

        public Dictionary<int, string>? Regions { get; set; }

        [StringLength(100)]
        public string BusinessName { get; set; }

        [StringLength(200)]
        public string BusinessWebsite { get; set; }

        public IFormFile Photo { get; set; }

        [StringLength(500)]
        public string AdminNotes { get; set; }

        public bool IsAgreementDoc { get; set; }

        public string AgreementDocPath { get; set; }

        [RequiredIfBoolIsTrue(nameof(IsAgreementDoc))]
        public IFormFile? AgreementDoc { get; set; }

        public bool IsBackgroundDoc { get; set; }

        public string BackgroundDocPath { get; set; }

        [RequiredIfBoolIsTrue(nameof(IsBackgroundDoc))]
        public IFormFile? BackgroundDoc { get; set; }

        public bool IsHIPAACompliance { get; set; }

        public string HIPAACompliancePath { get; set; }

        [RequiredIfBoolIsTrue(nameof(IsHIPAACompliance))]
        public IFormFile? HIPAACompliance { get; set; }

        public bool IsNonDisclosureDoc { get; set; }

        public string NonDisclosureDocPath { get; set; }

        [RequiredIfBoolIsTrue(nameof(IsNonDisclosureDoc))]
        public IFormFile? NonDisclosureDoc { get; set; }

        public string SignaturePath { get; set; }

        public bool IsSignature { get; set; }   

        public IFormFile? Signature { get; set; }
    }

    public class RequiredIfBoolIsTrueAttribute : ValidationAttribute
    {
        private readonly string[] _boolProperties;

        public RequiredIfBoolIsTrueAttribute(params string[] boolProperties)
        {
            _boolProperties = boolProperties;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            foreach (var boolProperty in _boolProperties)
            {
                var property = validationContext.ObjectType.GetProperty(boolProperty);
                var boolPropertyValue = (bool)property.GetValue(validationContext.ObjectInstance);
                if (boolPropertyValue && value == null)
                {
                    return new ValidationResult($"{validationContext.DisplayName} is required");
                }
            }
            return ValidationResult.Success;
        }
    }

    public class ProviderScheduling
    {
        public List<SchedulingTable> TableData { get; set; }

        public CreateShift CreateShift { get; set; }

    }

    public class SchedulingTable
    {
        public int PhysicianId { get; set; }

        public string Photo { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<ShiftDetailsDayWise> DayWise { get; set; }

        public Dictionary<int, double> WeekWise { get; set; }

    }

    public class ShiftDetailsDayWise
    {
        public int ShiftDetailsId { get; set; }

        public bool FirstHalf { get; set; } = false;

        public bool SecoundHalf { get; set; } = false;

        public String Status { get; set; }

        public int Time { get; set; }
    }

    public class SchedulingTableMonthWise
    {
        public int ShiftDetailsId { get; set; }

        public Dictionary<int, List<ShiftDetailsMonthWise>> MonthWise { get; set; }

        public int StartDate { get; set; }

        public int TotalDays { get; set; }

    }

    public class ShiftDetailsMonthWise
    {
        public int ShiftDetailsId { get; set; }

        public string ProviderName { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public String Status { get; set; }

    }

    public class CreateShift
    {
        [Display(Name = "Region")]
        public int SelectedRegion { get; set; }

        public Dictionary<int, string>? Regions { get; set; }

        [Display(Name = "Physician")]
        public int SelectedPhysician { get; set; }

        [Required]
        public DateTime? ShiftDate { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public bool IsRepeat { get; set; }

        public List<int>? SelectedDays { get; set; }

        public int RepeatEnd { get; set; } = 0;

        public string MinDate = DateOnly.FromDateTime(DateTime.Now).ToString("yyy-MM-dd");
    }

    public class RequestedShift
    {
        public Dictionary<int, string>? Regions { get; set; }

        public RequestShiftModel RequestedShiftModel { get; set; }
    }

    public class RequestShiftModel
    {
        public List<RequestedShiftTable> RequestedShiftTables { get; set; }

        public int TotalShifts { get; set; }

        public int PageNo { get; set; }

        public bool IsFirstPage { get; set; }

        public bool IsLastPage { get; set; }

        public bool IsNextPage { get; set; }

        public bool IsPreviousPage { get; set; }

        public int StartRange { get; set; }

        public int EndRange { get; set; }
    }

    public class RequestedShiftTable
    {
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public string Region { get; set; }

        public int ShiftDetailsId { get; set; }
    }

    public class ViewShift
    {
        public string ShiftDetailsId { get; set; }

        public string Region { get; set; }

        public string PhysicianName { get; set; }

        public DateOnly ShiftDate { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

    }

    public class ProviderOnCall
    {
        public Dictionary<int, string>? Regions { get; set; }

        public ProviderList ProviderList { get; set; }
    }

    public class ProviderList
    {
        public List<ProviderOnCallTable> providerOnCall { get; set; }

        public List<ProviderOnCallTable> providerOffDuty { get; set; }

    }

    public class ProviderOnCallTable
    {
        public string Photo { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
