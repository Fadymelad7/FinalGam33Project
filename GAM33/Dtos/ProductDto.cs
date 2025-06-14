using Gma33.Core.Entites;

namespace GAM33.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public string Details { get; set; }

        public string? ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public string Category { get; set; }
    }
}
