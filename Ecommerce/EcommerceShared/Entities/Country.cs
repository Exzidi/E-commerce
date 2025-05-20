using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Shared.Entities
{
    public class Country
    {
        public int Id { get; set; }

        [Display(Name = "Pais")]
        [Required(ErrorMessage = "El campo {0} Es obligatorio")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Display(Name="Estados/Departamentos")]
        public ICollection<State> States { get; set; }
        public int StateNumber=>States==null? 0:States.Count();

    }
}