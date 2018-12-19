using System.Collections.Generic;
using System.Linq;
using hcm.Database;

namespace hcm.Controllers
{
    public class ListResModel<T>
    {
        public PagingHeader Paging { get; set; }
        public List<LinkInfo> Links { get; set; }
        public List<T> Items { get; set; }
    }
}