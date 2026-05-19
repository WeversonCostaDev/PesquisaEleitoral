using System.Security.Principal;

namespace PesquisaEleitoral_v2.Pagination
{
    public interface IQueryStringPagination
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
    }
}
