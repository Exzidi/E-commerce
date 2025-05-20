using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Shared.Entities
{
    public class City
    {
        public int Id { get; set; }

        [Display(Name = "Ciudad")]
        [Required(ErrorMessage = "El campo {0} Es obligatorio")]
        [MaxLength(100)]
        public string Name { get; set; }

        public State State { get; set; }

        public int StateId { get; set; }
        public ICollection<User> ? Users { get; set;}
    }
}