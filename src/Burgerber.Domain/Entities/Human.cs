using System.ComponentModel.DataAnnotations;

namespace Burgerber.Domain.Entities
{
    public class Human : Auditable
    {
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        public DateTime Birthdate { get; set; }
    }
}
