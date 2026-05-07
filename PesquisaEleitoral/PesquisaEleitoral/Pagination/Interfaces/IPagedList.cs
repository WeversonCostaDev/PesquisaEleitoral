using System.Collections;

namespace PesquisaEleitoral.Pagination.Interfaces
{
    public interface IPagedList<T> : IList<T>
    {
        int CurrentPage { get;}
        int TotalPages { get;}
        bool HasPrevious { get; }
        bool HasNext { get; }
    }
}
