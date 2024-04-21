using System.ComponentModel.DataAnnotations;

namespace Services.ViewModels.Admin
{
    public class AdminCreaateAndProfile
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z]+(?: [a-zA-Z]+)*$", ErrorMessage = "The FirstName is not valid")]
        public string FirstName { get; set; }

        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z]+(?: [a-zA-Z]+)*$", ErrorMessage = "The LastName is not valid")]
        public string LastName { get; set; }

        [StringLength(50)]
        [DataType(DataType.EmailAddress, ErrorMessage = "The Email is not valid")]
        public string Email { get; set; }

        [StringLength(50)]
        [CompareAttribute("Email", ErrorMessage = "Email doesn't match.")]
        public string ConfirmEmail { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(20)]
        [RegularExpression(@"^(?:\+?91)?\s*([1-9]\d{4})\s*(\d{5})$", ErrorMessage = "The Mobile is not valid")]
        public string Mobile { get; set; }

        [StringLength(500)]
        public string Address1 { get; set; }

        [StringLength(500)]
        public string Address2 { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(10)]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "The State field is required.")]
        public string SelectedRegion { get; set; } 

        [Required(ErrorMessage = "The Region field is required.")]
        public List<string> SelectedRegions { get; set; }

        public Dictionary<int, string>? Regions { get; set; }

        public Dictionary<int, bool>? AdminRegions { get; set; }

        public short? Status { get; set; }

        [Required(ErrorMessage = "The Role field is required.")]
        public string SelectedRole { get; set; }

        public List<string>? Roles { get; set; }

    }
}
