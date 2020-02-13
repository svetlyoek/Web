namespace IRunes.App.Controllers
{
    using IRunes.App.ViewModels.Users;
    using IRunes.Services;
    using SIS.HTTP;
    using SIS.MvcFramework;
    public class HomeController : Controller
    {
        private readonly IUsersService userService;

        public HomeController(IUsersService userService)
        {
            this.userService = userService;
        }

        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (this.IsUserLoggedIn())
            {
                var username = this.userService.GetUsername(this.User);
                var userViewModel = new UserHomePageViewModel
                {
                    Username = username
                };
                return this.View(userViewModel, "Home");
            }

            return this.View();
        }

        [HttpGet("/Home/Index")]
        public HttpResponse IndexSlash()
        {
            return this.Index();
        }

    }
}
