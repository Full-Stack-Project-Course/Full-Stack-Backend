using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class CustomBasketDto
    {
        [Required]
        public string Id { get; set; } = string.Empty;

        [Required]
        public List<BasketItemDto> Items { get; set; }
    }
}
