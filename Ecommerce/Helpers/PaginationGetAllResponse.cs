using Ecommerce.Dtos;

namespace Ecommerce.Helpers
{
    public class PaginationGetAllResponse <T>
    {
       

        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }


        public PaginationGetAllResponse(int pageSize, int pageIndex,int count, IReadOnlyList<T> data)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            Data = data;
            Count = count;
        }
    }
}
