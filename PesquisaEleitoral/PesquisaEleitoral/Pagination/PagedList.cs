using PesquisaEleitoral.Pagination.Interfaces;

namespace PesquisaEleitoral.Pagination
{
    public class PagedList<T> : List<T>, IPagedList<T> where T :class
    {
        public int CurrentPage { get;}
        public int TotalPages { get;}
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PagedList(IEnumerable<T> items, int count, int pageSize, int pageNumber)
        {
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }
    }
}
