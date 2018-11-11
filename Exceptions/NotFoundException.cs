using System;

namespace hcm.Exceptions
{
    public class NotFoundException : Exception
    {
        public int Status { get; private set; }

        public NotFoundException(string message) : base(message)
        {
            Status = 404;
        }

    }

}