namespace SIS.HTTP.Exceptions
{
    using System;

    public class InternalServerErrorException : Exception
    {
        public const string ServerErrorMessage = "The Server has encountered an error.";

        public InternalServerErrorException()
            : this(ServerErrorMessage)
        {
        }

        public InternalServerErrorException(string name)
            : base(name)
        {
        }

    }
}
