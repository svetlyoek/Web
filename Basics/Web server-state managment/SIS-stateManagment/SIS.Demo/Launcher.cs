namespace SIS.Demo
{
    using SIS.Demo.Controllers;
    using SIS.HTTP.Enums;
    using SIS.WebServer;
    using SIS.WebServer.Routing;
    using SIS.WebServer.Routing.Contracts;

    public class Launcher
    {
        public static void Main(string[] args)
        {
            IServerRoutingTable routingTable = new ServerRoutingTable();

            routingTable.Add(HttpRequestMethod.Get, "/", request => new HomeController().Home(request));
            routingTable.Add(HttpRequestMethod.Get, "/login", request => new HomeController().Login(request));
            routingTable.Add(HttpRequestMethod.Get, "/logout", request => new HomeController().LogOut(request));

            Server server = new Server(8000, routingTable);

            server.Run();

        }
    }
}
