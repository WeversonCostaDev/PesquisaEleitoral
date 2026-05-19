namespace PesquisaEleitoral_v2.Pagination
{
    public interface IPagedList<T> : IList<T>
    {
        int CurrentPage { get;}
        int TotalPage { get;}
        bool HasPrevious { get;}
        bool HasNext {get;}
    }
}
