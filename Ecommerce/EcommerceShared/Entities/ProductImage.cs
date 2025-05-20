using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Shared.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string Image { get; set; }
        // Relación 1:1 con Product
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
