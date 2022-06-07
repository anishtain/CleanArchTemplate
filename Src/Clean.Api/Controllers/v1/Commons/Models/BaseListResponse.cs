namespace Clean.Api.Controllers.v1.Commons.Models
{
    public class BaseListResponse<T>
    {
        public BaseListResponse(int total, int pageSize, int page, IList<T> list)
        {
            Total = total;
            PageSize = pageSize;
            Page = page;
            List = list;
        }

        public int Total { get; set; }

        public int PageSize { get; set; }

        public int Page { get; set; }

        public IList<T> List { get; set; }
    }
}
