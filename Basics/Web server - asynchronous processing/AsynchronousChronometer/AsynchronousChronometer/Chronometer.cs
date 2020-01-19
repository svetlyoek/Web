namespace AsynchronousChronometer
{
    using AsynchronousChronometer.Contracts;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class Chronometer : IChronometer
    {
        private long milliseconds;

        private bool isRunning;

        public Chronometer()
        {
            this.Laps = new List<string>();
        }

        public string GetTime =>
            $"{this.milliseconds / 60000:d2}:{milliseconds / 1000:d2}:{(milliseconds % 1000):d4}";

        public List<string> Laps { get; }


        public string Lap()
        {
            string lap = this.GetTime;
            this.Laps.Add(lap);

            return lap;
        }

        public void Reset()
        {
            this.Stop();
            this.milliseconds = 0L;
            this.Laps.Clear();
        }

        public void Start()
        {
            this.isRunning = true;

            Task.Run(() =>
            {
                while (isRunning)
                {
                    Thread.Sleep(1);

                    this.milliseconds++;
                }

            });

        }

        public void Stop()
        {
            this.isRunning = false;

        }
    }
}
