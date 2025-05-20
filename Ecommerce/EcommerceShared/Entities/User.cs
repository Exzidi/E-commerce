using System.ComponentModel.DataAnnotations;
using Ecommerce.Shared.Enums;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Shared.Entities
{
    public class User:IdentityUser
    {
        [MaxLength(100)]
        [Required(ErrorMessage = "El campo es Obligatorio")]
        [Display(Name = "Documento")]
        public string Document { get; set; }
        [Display(Name = "El documento es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} es obligatorio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Apellidos")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener minimo {1} caractéres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]

        public string LastName { get; set; } = null!;

        [Display(Name = "Dirección")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener minimo {1} caractéres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]

        public string Address { get; set; } = null!;

        [Display(Name = "Tipos de usuario")]
        public UserType userType { get; set; }
        public City? city { get; set; }
        [Display(Name = "Ciudad")]
        [Range(1, int.MinValue,ErrorMessage = "Debes seleccionar una {0}.")]
        public int CityId { get; set; }

        [Display(Name = "Usuario")]

        public string FullName => $"{FirstName}{LastName}";
    }
}
