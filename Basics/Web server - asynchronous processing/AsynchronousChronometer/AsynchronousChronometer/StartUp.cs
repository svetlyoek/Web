namespace AsynchronousChronometer
{
    using AsynchronousChronometer.Contracts;
    using System;
    using System.Linq;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            IChronometer chronometer = new Chronometer();

            while (true)
            {
                string command = Console.ReadLine();

                if (command == "exit")
                {
                    break;
                }
                if (command == "start")
                {
                    chronometer.Start();
                }
                if (command == "stop")
                {
                    chronometer.Stop();
                }
                if (command == "lap")
                {
                    Console.WriteLine(chronometer.Lap());
                }
                if (command == "laps")
                {
                    Console.WriteLine($"Laps:" + (!chronometer.Laps.Any() ? "no laps"
                        : "\r\n" 
                        + string.Join("\r\n",
                        chronometer.Laps
                        .Select((lap, index) => $"{index}. { lap}")
                        )));
                }

                if (command == "time")
                {
                    Console.WriteLine(chronometer.GetTime);
                }
                if (command == "reset")
                {
                    chronometer.Reset();
                }
            }


        }
    }
}
