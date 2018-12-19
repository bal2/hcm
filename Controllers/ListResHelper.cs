using System;
using System.Collections.Generic;
using hcm.Database;
using hcm.Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace hcm.Controllers
{
    //Helper class for generating PagingHeader and LinkInfo for List responses
    static class ListResHelper
    {
        public static PagingHeader GenerateHeaderFromPagedList(IPagedListProperties p)
        {
            return new PagingHeader(p.TotalItems, p.PageNumber, p.PageSize, p.TotalPages);
        }

        public static List<LinkInfo> GenerateLinksFromPagedList(IUrlHelper u, string routeName, IPagedListProperties p)
        {
            var links = new List<LinkInfo>();

            if (p.HasPreviousPage)
                links.Add(createLink(u, routeName, p.PreviousPageNumber, p.PageSize, "previousPage", "GET"));

            links.Add(createLink(u, routeName, p.PageNumber, p.PageSize, "self", "GET"));

            if (p.HasNextPage)
                links.Add(createLink(u, routeName, p.NextPageNumber, p.PageSize, "nextPage", "GET"));

            return links;
        }

        private static LinkInfo createLink(IUrlHelper Url, string routeName, int pageNumber, int pageSize, string rel, string method)
        {
            return new LinkInfo
            {
                Href = Url.Link(routeName,
                            new { PageNumber = pageNumber, PageSize = pageSize }),
                Rel = rel,
                Method = method
            };
        }
    }
}