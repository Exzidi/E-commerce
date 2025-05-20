using System.Collections;
using System.ComponentModel.DataAnnotations;
using Ecommerce.Shared.Enums;

namespace Ecommerce.Shared.Entities
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }


        [Display(Name = "CategoriaProducto")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100)]
        
        public string Name { get; set; }

        public ICollection<Category> Categories { get; set; }

        [Display(Name = "Categorías")]

        public int CategoriesNumber => Categories == null ? 0: Categories.Count;

        public ICollection<Product> Products { get; set; }

        [Display(Name = "Productos")]
        public int ProductsNumber => Products ==null ? 0 : Products.Count();

        [Display(Name = "Productos")]
        public ICollection<ProductImage> ProductImages { get; set; }

    }
}
