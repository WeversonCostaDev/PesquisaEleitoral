namespace PesquisaEleitoral_v2.Pagination
{
    public class QueryStringPagination : IQueryStringPagination
    {
        const int _maxPage = 50;
        private int _pageSize = _maxPage;
        public int PageNumber { get; set; } = 1;
        public int PageSize 
        { 
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > _maxPage) ? _maxPage : value;
            }
         }
    }
}
