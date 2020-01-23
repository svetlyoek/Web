namespace SIS.HTTP.Exceptions
{
    using System;

    public class BadRequestException : Exception
    {
        public const string BadRequestMessage = "The Request was malformed or contains unsupported elements.";

        public BadRequestException()
            : this(BadRequestMessage)
        {
        }
        public BadRequestException(string name)
            : base(name)
        {
        }
    }
}
