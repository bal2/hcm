using System;
using hcm.Exceptions;

namespace hcm.Controllers
{
    //Default query parameters for list resources. Can be extended to change defaults
    public class ListQueryArgs
    {
        public string Filter { get; set; }
        public string OrderBy { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public int MaxSize { get; } = 20;

        public void Validate()
        {
            if (PageSize > MaxSize || PageSize < 1)
                throw new BadRequestException("PageSize can not be bigger than " + MaxSize + " or less than 1");
        }
    }

}