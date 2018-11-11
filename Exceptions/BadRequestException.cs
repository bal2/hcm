using System;

namespace hcm.Exceptions
{
    public class BadRequestException : Exception
    {
        public int Status { get; private set; }

        public BadRequestException(string message) : base(message)
        {
            Status = 400;
        }

    }

}