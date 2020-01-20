namespace SIS.Demo
{
    using SIS.HTTP.Enums;
    using SIS.WebServer;
    using SIS.WebServer.Routing;
    using SIS.WebServer.Routing.Contracts;

    public class Launcher
    {
        public static void Main(string[] args)
        {
            IServerRoutingTable routingTable = new ServerRoutingTable();

            routingTable.Add(HttpRequestMethod.Get, "/", request => new HomeController().Index(request));

            Server server = new Server(8000, routingTable);

            server.Run();

        }
    }
}
