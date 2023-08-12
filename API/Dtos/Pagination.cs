using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Dtos
{
    public class Pagination<T> where T : class
    {
        public Pagination(int count , int pageSize , int pageIndex , IReadOnlyList<T> data)
        {
            Count = count ; 
            PageIndex = pageIndex ;
            PageSize = pageSize;
            Data = data ;
        }

        public int Count { get; set; }
        public int PageSize { get; set; }

        public int PageIndex { get; set; }
        public IReadOnlyList<T> Data { get; set; }




    }
}
