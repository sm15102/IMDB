namespace Imdb.Application.Common
{
    public abstract class PagedResult
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public bool HasMore { get; set; }
    }
}
