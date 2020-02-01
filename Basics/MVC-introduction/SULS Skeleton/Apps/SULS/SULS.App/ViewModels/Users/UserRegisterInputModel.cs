using SIS.MvcFramework.Attributes.Validation;

namespace SULS.App.ViewModels.Users
{
    public class UserRegisterInputModel
    {
        private const string UsernameErrorMessage = "Username must be between 5 and 20 symbols!";
        private const string PasswordErrorMessage = "Password must be between 6 and 20 symbols!";

        [RequiredSis]
        [StringLengthSis(5,20, UsernameErrorMessage)]
        public string Username { get; set; }

        [RequiredSis]
        [EmailSis]
        public string Email { get; set; }

        [RequiredSis(PasswordErrorMessage)]
        [PasswordSis(nameof(ConfirmPassword))]
        public string Password { get; set; }

        [RequiredSis(PasswordErrorMessage)]
        public string ConfirmPassword { get; set; }
    }
}
