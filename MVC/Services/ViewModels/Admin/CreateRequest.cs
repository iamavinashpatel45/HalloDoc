using System.ComponentModel.DataAnnotations;

namespace Services.ViewModels.Admin
{
    public class CreateRequest
    {
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z]+(?: [a-zA-Z]+)*$", ErrorMessage = "The FirstName is not valid")]
        public string FirstName { get; set; }

        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z]+(?: [a-zA-Z]+)*$", ErrorMessage = "The LastName is not valid")]
        public string LastName { get; set; }

        [StringLength(50)]
        [DataType(DataType.EmailAddress, ErrorMessage = "The Email is not valid")]
        public string Email { get; set; }

        [StringLength(20)]
        [RegularExpression(@"^(?:\+?91)?\s*([1-9]\d{4})\s*(\d{5})$", ErrorMessage = "The Mobile is not valid")]
        public string Mobile { get; set; }

        [StringLength(100)]
        public string Street { get; set; }

        public string? Password { get; set; }

        [CompareAttribute("Password", ErrorMessage = "The Password doesn't match.")]
        public string? ConformPassword { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(100)]
        public string State { get; set; }

        [StringLength(10)]
        public string ZipCode { get; set; }

        public string? Room { get; set; }

        [StringLength(500)]
        public string? Symptoms { get; set; }

        [Required]
        public DateTime? BirthDate { get; set; }
    }
}
