using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Shared.DTOs
{
    public class LoginDTO
    {
        
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [EmailAddress(ErrorMessage = "Debe ingresar un email valido")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MinLength(0, ErrorMessage = "El campo {0} debe tener almenos {1} caracteres")]
        public string Password { get; set; }
    }
}
