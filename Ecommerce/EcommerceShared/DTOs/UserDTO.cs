using Ecommerce.Shared.Entities;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Shared.DTOs
{
    public class UserDTO:User
    {
        [DataType(DataType.Password)]
        [Display(Name="Contraseña")]
        [Required(ErrorMessage="El campo {0} es requerido")]
        [StringLength(20,MinimumLength =6,ErrorMessage = "El campo {0} debe tener {2} y {1} caracteres")]
        public string Password { get; set; } = null!;

        [Compare("Password", ErrorMessage ="La contraseña de confirmacion debe ser igual")]
        [Display(Name = "Confirmacion Contraseña")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "El campo {0} debe tener {2} y {1} caracteres")]
        public string PasswordConfirm { get; set; } = null!;

    }
}
