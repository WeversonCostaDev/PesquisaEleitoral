namespace PesquisaEleitoral_v2.Pagination
{
    public class PagedList<T> : List<T>, IPagedList<T> where T : class
    {
        public int CurrentPage { get; }
        public int TotalPage { get; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPage;

        public PagedList(IEnumerable<T> items, int count, int pageSize, int pageNumber)
        {
            CurrentPage = pageNumber;
            TotalPage = (int)Math.Ceiling(count / (decimal)pageSize);

            AddRange(items);
        }
        public PagedList(int totalPage, int currentPage, IEnumerable<T> items)
        {
            CurrentPage = currentPage;
            TotalPage = totalPage;

            AddRange(items);
        }
    }
}
