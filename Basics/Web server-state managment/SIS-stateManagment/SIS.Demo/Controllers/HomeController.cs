namespace SIS.Demo.Controllers
{
    using SIS.HTTP.Requests;
    using SIS.HTTP.Responses;
    public class HomeController : BaseController
    {
        public IHttpResponse Home(IHttpRequest httpRequest)
        {
            this.HttpRequest = httpRequest;
            return this.View();
        }
        public IHttpResponse Login(IHttpRequest httpRequest)
        {
            httpRequest.Session.AddParameter("username", "Svetoslav");

            return this.Redirect("/");
        }
        public IHttpResponse LogOut(IHttpRequest httpRequest)
        {
            httpRequest.Session.ClearParameters();

            return this.Redirect("/");
        }
    }
}
