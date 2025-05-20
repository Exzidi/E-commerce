using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Shared.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Display(Name = "Producto")]
        [Required(ErrorMessage = "El campo {0} Es obligatorio")]
        [MaxLength(100)]
        public string Name { get; set; }

        public double Price { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public int ProductCategoryId { get; set; }
        public ProductImage ProductImage { get; set; }
        public int ProductImageId { get; set; }

    }
}
