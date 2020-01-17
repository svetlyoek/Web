namespace HttpRequester
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;

    public class Program
    {
        static async Task Main(string[] args)
        {
            const string NewLine = "\r\n";

            TcpListener tcpListener = new TcpListener(IPAddress.Loopback,80);
            tcpListener.Start();

            while (true)
            {
                TcpClient tcpClient = tcpListener.AcceptTcpClient();

                using NetworkStream networkStream = tcpClient.GetStream();
                byte[] requestedBytes = new byte[1000000];
                int bytesRead = networkStream.Read(requestedBytes, 0, requestedBytes.Length);
                string request = Encoding.UTF8.GetString(requestedBytes, 0, bytesRead);
                string responseText = @"<form action='Action/Login' method='post'>
                <input type='date' name='date'/>
                <input type='text' name='username'/>
                <input type='password' name='password' /> 
                <input type='submit' name='Login'/>
                </form>";
                string response = "HTTP/1.0 200 OK" + NewLine +
                                  "Server: SoftUniServer/1.0" + NewLine +
                                  "Content-Type: text/html" + NewLine +
                                   "Location: https://abv.bg" + NewLine +
                                  //"Content-Disposition: attachment; filename=svetlyo.pdf" + NewLine +
                                  "Content-Lenght: " + responseText.Length + NewLine +
                                  NewLine +
                                  responseText;
                byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                networkStream.Write(responseBytes, 0, responseBytes.Length);
                Console.WriteLine(request);
                Console.WriteLine(new string('=', 60));
            }
        }
        public static async Task HttpRequest()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://softuni.bg/");
            string result = await response.Content.ReadAsStringAsync();
            File.WriteAllText("index.html", result);
        }
    }
}
