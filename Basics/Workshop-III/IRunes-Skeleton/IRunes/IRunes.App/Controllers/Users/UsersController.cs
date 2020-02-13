namespace IRunes.App.Controllers
{
    using IRunes.App.ViewModels.Users;
    using IRunes.Services;
    using SIS.HTTP;
    using SIS.MvcFramework;
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public HttpResponse Login()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(UserInputViewModel inputModel)
        {
            var userId = this.usersService.GetUserId(inputModel.Username, inputModel.Password);

            if (userId != null)
            {
                this.SignIn(userId);

                return this.Redirect("/");

            }

            return this.Redirect("/Users/Login");
        }


        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(UserRegisterViewModel inputModel)
        {
            if (inputModel.Username.Length < 4 || inputModel.Username.Length > 10)
            {
                return this.Error("Username must be between 4 and 10 characters long!");
            }

            if (string.IsNullOrWhiteSpace(inputModel.Email))
            {
                return this.Error("Email is required!");
            }
            if (string.IsNullOrWhiteSpace(inputModel.Password))
            {
                return this.Error("Passowrd is required!");
            }
            if (inputModel.Password.Length < 6 || inputModel.Password.Length > 20)
            {
                return this.Error("Password must be between 6 and 20 characters long!");
            }

            if (inputModel.Password != inputModel.ConfirmPassword)
            {
                return this.Error("Passwords should match!");
            }
            if (this.usersService.UsernameExists(inputModel.Username))
            {
                return this.Error("Username already exists!");
            }
            if (this.usersService.EmailExists(inputModel.Email))
            {
                return this.Error("Email already exists!");
            }

            this.usersService.Register(inputModel.Username, inputModel.Email, inputModel.Password);

            return this.Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            this.SignOut();
            return this.Redirect("/");
        }
    }
}
