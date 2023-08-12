namespace Infrastructure.Data
{
    public class ProductSpecificationParams
    {
        public string? sortby { get; set; }
        public string? search { get; set; }
        public int? brandID { get; set; }



        private int MaxCount { get; set; } = 50;


        public void setMaxPage(int maxCount)
        {
            MaxCount = maxCount;
        }
        public int? typeID { get; set; }

        private int PageSize { get; set; } = 6;

        private int PageIndex { get; set; } = 1;
        public int pageSize { 
            get => PageSize = (PageSize > MaxCount) ? MaxCount : PageSize; 
            set => PageSize = (value > MaxCount) ? MaxCount : value;

            }

        public int pageIndex
        {
            get => PageIndex;
            set => PageIndex = value > 0 ? value : 1;

        }
        // public int? pageSize { get; set; }


    }
}
