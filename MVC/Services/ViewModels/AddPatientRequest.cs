using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Services.ViewModels
{
    public class AddPatientRequest
    { 
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z]+(?: [a-zA-Z]+)*$", ErrorMessage = "The FirstName is not valid")]
        public string FirstName { get; set; } 

        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z]+(?: [a-zA-Z]+)*$", ErrorMessage = "The LastName is not valid")]
        public string LastName { get; set; }

        [StringLength(50)]
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "The Email is not valid")]
        public string Email { get; set; } 

        [StringLength(100)]
        public string? Password { get; set; }

        [StringLength(100)]
        [CompareAttribute("Password", ErrorMessage = "The Password doesn't match.")]
        public string? ConformPassword { get; set; }

        [StringLength(20)]
        [Required]
        [RegularExpression(@"^(?:\+?91)?\s*([1-9]\d{4})\s*(\d{5})$", ErrorMessage = "The Mobile is not valid")]
        public string Mobile { get; set; }

        [StringLength(100)]
        [Required]
        public string Street { get; set; }

        [StringLength(100)]
        [Required]
        public string City { get; set; }

        [StringLength(100)]
        [Required]
        public string State { get; set; }

        [StringLength(10)]
        [Required]
        public string ZipCode { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public IFormFile? File { get; set; }

        [StringLength(100)]
        public string? House { get; set; }

        [StringLength(500)]
        public string? Symptoms { get; set; }
    }

    public class AddFamilyRequest
    {
        [StringLength(100)]
        [Required]
        [Display(Name = "FirstName")]
        [RegularExpression(@"^[a-zA-Z]+(?: [a-zA-Z]+)*$", ErrorMessage = "The FirstName is not valid")]
        public string FamilyFriendFirstName { get; set; }

        [StringLength(100)]
        [Required]
        [Display(Name = "LastName")]
        [RegularExpression(@"^[a-zA-Z]+(?: [a-zA-Z]+)*$", ErrorMessage = "The LastName is not valid")]
        public string? FamilyFriendLastName { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Email")]
        public string FamilyFriendEmail { get; set; }

        [StringLength(20)]
        [Required]
        [Display(Name = "Mobile")]
        [RegularExpression(@"^(?:\+?91)?\s*([1-9]\d{4})\s*(\d{5})$", ErrorMessage = "The Mobile is not valid")]
        public string? FamilyFriendMobile { get; set; }

        [StringLength(100)]
        [Required]
        public string? Relation { get; set; }

        [StringLength(100)]
        [Required]
        [RegularExpression(@"^[a-zA-Z]+(?: [a-zA-Z]+)*$", ErrorMessage = "The FirstName is not valid")]
        public string FirstName { get; set; }

        [StringLength(100)]
        [Required]
        [RegularExpression(@"^[a-zA-Z]+(?: [a-zA-Z]+)*$", ErrorMessage = "The LastName is not valid")]
        public string? LastName { get; set; }

        [StringLength(50)]
        [Required]
        public string Email { get; set; }

        [StringLength(100)]
        public string? Password { get; set; }

        [StringLength(100)]
        [CompareAttribute("Password", ErrorMessage = "The Password doesn't match.")]
        public string? ConformPassword { get; set; }

        [StringLength(20)]
        [Required]
        [RegularExpression(@"^(?:\+?91)?\s*([1-9]\d{4})\s*(\d{5})$", ErrorMessage = "The Mobile is not valid")]
        public string? Mobile { get; set; }

        [StringLength(100)]
        [Required]
        public string? Street { get; set; }

        [StringLength(100)]
        [Required]
        public string? City { get; set; }

        [StringLength(100)]
        [Required]
        public string? State { get; set; }

        [StringLength(10)]
        [Required]
        public string? ZipCode { get; set; }

        [StringLength(500)]
        public string? Symptoms { get; set; }

        [StringLength(100)]
        public string? House { get; set; }

        [Required]
        public DateTime? BirthDate { get; set; }

        public IFormFile? File { get; set; }
    }

    public class AddConciergeRequest
    {
        [StringLength(100)]
        [Required]
        [Display(Name = "FirstName")]
        [RegularExpression(@"^[a-zA-Z]+(?: [a-zA-Z]+)*$", ErrorMessage = "The FirstName is not valid")]
        public string ConciergeFirstName { get; set; } = null!;

        [StringLength(100)]
        [Required]
        [Display(Name = "LastName")]
        [RegularExpression(@"^[a-zA-Z]+(?: [a-zA-Z]+)*$", ErrorMessage = "The LastName is not valid")]
        public string ConciergeLastName { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "The Email is not valid")]
        public string ConciergeEmail { get; set; } = null!;

        [StringLength(20)]
        [Required]
        [Display(Name = "Mobile")]
        [RegularExpression(@"^(?:\+?91)?\s*([1-9]\d{4})\s*(\d{5})$", ErrorMessage = "The Mobile is not valid")]
        public string ConciergeMobile { get; set; }

        [StringLength(100)]
        [Required]
        [Display(Name = "PropertyName")]
        public string ConciergePropertyName { get; set; }

        [StringLength(100)]
        [Required]
        [Display(Name = "Street")]
        public string ConciergeStreet { get; set; }

        [StringLength(100)]
        [Required]
        [Display(Name = "City")]
        public string ConciergeCity { get; set; }

        [StringLength(100)]
        [Required]
        [Display(Name = "State")]
        public string? ConciergeState { get; set; }

        [StringLength(10)]
        [Required]
        [Display(Name = "ZipCode")]
        public string? ConciergeZipCode { get; set; }

        [StringLength(100)]
        [Required]
        [RegularExpression(@"^[a-zA-Z]+(?: [a-zA-Z]+)*$", ErrorMessage = "The FirstName is not valid")]
        public string FirstName { get; set; }

        [StringLength(100)]
        [Required]
        [RegularExpression(@"^[a-zA-Z]+(?: [a-zA-Z]+)*$", ErrorMessage = "The LastName is not valid")]
        public string? LastName { get; set; }

        [StringLength(50)]
        [Required]
        public string Email { get; set; }

        [StringLength(100)]
        public string? Password { get; set; }

        [StringLength(100)]
        [CompareAttribute("Password", ErrorMessage = "The Password doesn't match.")]
        public string? ConformPassword { get; set; }

        [StringLength(20)]
        [Required]
        [RegularExpression(@"^(?:\+?91)?\s*([1-9]\d{4})\s*(\d{5})$", ErrorMessage = "The Mobile is not valid")]
        public string? Mobile { get; set; }

        [StringLength(100)]
        [Required]
        public string? Street { get; set; }

        [StringLength(100)]
        [Required]
        public string? City { get; set; }

        [StringLength(100)]
        [Required]
        public string? State { get; set; }

        [StringLength(10)]
        [Required]
        public string? ZipCode { get; set; }

        [StringLength(500)]
        public string? Symptoms { get; set; }

        [StringLength(100)]
        public string? House { get; set; }

        [Required]
        public DateTime? BirthDate { get; set; }

        public IFormFile? File { get; set; }

    }

    public class AddBusinessRequest
    {
        [StringLength(100)]
        [Required]
        [Display(Name = "FirstName")]
        [RegularExpression(@"^[a-zA-Z]+(?: [a-zA-Z]+)*$", ErrorMessage = "The FirstName is not valid")]
        public string BusinessFirstName { get; set; }

        [StringLength(100)]
        [Required]
        [Display(Name = "LastName")]
        [RegularExpression(@"^[a-zA-Z]+(?: [a-zA-Z]+)*$", ErrorMessage = "The LastName is not valid")]
        public string? BusinessLastName { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "The Email is not valid")]
        public string BusinessEmail { get; set; } = null!;

        [StringLength(20)]
        [Required]
        [Display(Name = "Mobile")]
        [RegularExpression(@"^(?:\+?91)?\s*([1-9]\d{4})\s*(\d{5})$", ErrorMessage = "The Mobile is not valid")]
        public string? BusinessMobile { get; set; }

        [StringLength(100)]
        [Required]
        public string? Business { get; set; }

        [StringLength(100)]
        public string? CaseNumber { get; set; }

        [StringLength(100)]
        [Required]
        [RegularExpression(@"^[a-zA-Z]+(?: [a-zA-Z]+)*$", ErrorMessage = "The FirstName is not valid")]
        public string FirstName { get; set; } = null!;

        [StringLength(100)]
        [Required]
        [RegularExpression(@"^[a-zA-Z]+(?: [a-zA-Z]+)*$", ErrorMessage = "The LastName is not valid")]
        public string? LastName { get; set; }

        [StringLength(50)]
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "The Email is not valid")]
        public string Email { get; set; } = null!;

        [StringLength(100)]
        public string? Password { get; set; }

        [StringLength(100)]
        [CompareAttribute("Password", ErrorMessage = "The Password doesn't match.")]
        public string? ConformPassword { get; set; }

        [StringLength(20)]
        [Required]
        [RegularExpression(@"^(?:\+?91)?\s*([1-9]\d{4})\s*(\d{5})$", ErrorMessage = "The Mobile is not valid")]
        public string? Mobile { get; set; }

        [StringLength(100)]
        [Required]
        public string? Street { get; set; }

        [StringLength(100)]
        [Required]
        public string? City { get; set; }

        [StringLength(100)]
        [Required]
        public string? State { get; set; }

        [StringLength(10)]
        [Required]
        public string ZipCode { get; set; }

        [StringLength(500)]
        public string? Symptoms { get; set; }

        [StringLength(100)]
        public string? House { get; set; }

        [Required]
        public DateTime? BirthDate { get; set; }

        public IFormFile? File { get; set; }
    }

    public class AddRequestByPatient
    {
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z]+(?: [a-zA-Z]+)*$", ErrorMessage = "The FirstName is not valid")]
        public string FirstName { get; set; }

        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z]+(?: [a-zA-Z]+)*$", ErrorMessage = "The LastName is not valid")]
        public string LastName { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(100)]
        public string? Password { get; set; }

        [StringLength(100)]
        [CompareAttribute("Password", ErrorMessage = "The Password doesn't match.")]
        public string? ConformPassword { get; set; }

        [StringLength(20)]
        [RegularExpression(@"^(?:\+?91)?\s*([1-9]\d{4})\s*(\d{5})$", ErrorMessage = "The Mobile is not valid")]
        public string Mobile { get; set; }

        [StringLength(100)]
        public string Street { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(100)]
        public string State { get; set; }

        [StringLength(10)]
        public string ZipCode { get; set; }

        public string Relation { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public IFormFile? File { get; set; }

        [StringLength(100)]
        public string? House { get; set; }

        [StringLength(500)]
        public string? Symptoms { get; set; }
    }
}
