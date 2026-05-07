using PesquisaEleitoral.Pagination.Interfaces;

namespace PesquisaEleitoral.Pagination
{
    public class QueryStringPagination : IQueryStringPagination
    {
        const int max_Size = 50;
        private int page_Size = max_Size;
        public int PageNumber { get; set; } = 1;
        public int PageSize 
        {
            get
            {
                return page_Size;
            }
            set
            {
                page_Size = (value > max_Size) ? max_Size : value;
            }
        }

    }
}
