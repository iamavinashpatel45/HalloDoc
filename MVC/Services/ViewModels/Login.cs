using System.ComponentModel.DataAnnotations;

namespace Services.ViewModels
{
    public class Login
    {
        //[RegularExpression("(?=^.{8,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+}{\":;'?/>.<,])(?!.*\\s).*$", 
        //    ErrorMessage = "Password must be 8-10 characters long with at least one numeric,one upper case character and one special character.")]
        [Display(Name = "Password")]
        public string PasswordHash { get; set; }

        public string Email { get; set; }
    }

    public class ResetPassword
    {
        public string Email { get; set; }
    }

    public class SetNewPassword
    {
        public bool IsValidLink { get; set; }

        public string? ErrorMessage { get; set; }

        public int AspNetUserId { get; set; }

        [Display(Name = "Password")]
        //[RegularExpression("(?=^.{8,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+}{\":;'?/>.<,])(?!.*\\s).*$",
        //    ErrorMessage = "Password must be 8-10 characters long with at least one numeric,one upper case character and one special character.")]
        public string Password { get; set; }

        [Display(Name = "Conform Password")]
        //[RegularExpression("(?=^.{8,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+}{\":;'?/>.<,])(?!.*\\s).*$",
        //    ErrorMessage = "Password must be 8-10 characters long with at least one numeric,one upper case character and one special character.")]
        [CompareAttribute("Password", ErrorMessage = "The Password doesn't match.")]
        public string ConformPassword { get; set; }

    }

    public class UserDataModel
    {
        public bool IsValid { get; set; } = false;

        public int AspNetUserId { get; set; }

        public int UserTypeId { get; set; }

        public string UserType { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Message { get; set; }
    }
}
