namespace Demo.App
{
    using SIS.HTTP.Enums;
    using SIS.HTTP.Headers;
    using SIS.HTTP.Requests;
    using SIS.HTTP.Responses;
    using System;
    using System.Text;

    public class Program
    {
        public static void Main(string[] args)
        {
            //string request = "POST /url/asd?name=ivan&password=123#fragment HTTP/1.1\r\n"
            //    + "Authorization:Basic\r\n"
            //    + "Host:localhost:5000\r\n"
            //    + "Date: " + DateTime.Now + "\r\n"
            //    + "\r\n"
            //    + "username=ivandim&password=666";

            //HttpRequest httpRequest = new HttpRequest(request);

            //HttpResponse response = new HttpResponse(HttpResponseStatusCode.Created);
            //response.AddHeader(new HttpHeader("Host", "localhost:8000"));
            //response.AddHeader(new HttpHeader("Date", DateTime.Now.ToString()));
            //response.Content = Encoding.UTF8.GetBytes("<h1> Hello World!</h1>");
            //Console.WriteLine(Encoding.UTF8.GetString(response.GetBytes()));

        }
    }
}
