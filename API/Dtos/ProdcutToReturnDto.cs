using Core.Entities;

namespace API.Dtos
{
    public class ProdcutToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public double Price { get; set; }
        public string ProductType { get; set; }
        public string ProductBrand { get; set; }
    }
}
