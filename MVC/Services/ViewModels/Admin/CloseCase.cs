using System.ComponentModel.DataAnnotations;

namespace Services.ViewModels.Admin
{
    public class CloseCase
    {
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z]+(?: [a-zA-Z]+)*$", ErrorMessage = "The FirstName is not valid")]
        public string FirstName { get; set; }

        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z]+(?: [a-zA-Z]+)*$", ErrorMessage = "The LastName is not valid")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "The Email is not valid")]
        public string? Email { get; set; }

        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "The Phone is not valid")]
        public string Phone { get; set; }

        [Required]
        public DateTime? BirthDate { get; set; }

        public List<FileModel>? FileList { get; set; }

    }
}
