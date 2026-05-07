namespace PesquisaEleitoral.Pagination.Interfaces
{
    public interface IQueryStringPagination
    {
        int PageSize { get; set; }
        int PageNumber { get; set; }
    }
}
