using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Shared.DTOs
{
    public class EmailDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [EmailAddress(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
