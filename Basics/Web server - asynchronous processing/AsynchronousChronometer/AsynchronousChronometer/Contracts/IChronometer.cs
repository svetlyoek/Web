namespace AsynchronousChronometer.Contracts
{
    using System.Collections.Generic;

    public interface IChronometer
    {
        string GetTime { get; }

        List<string> Laps { get; }

        void Start();

        void Stop();

        void Reset();

        string Lap();
    }
}
