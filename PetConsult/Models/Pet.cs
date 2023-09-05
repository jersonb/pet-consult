using System.ComponentModel.DataAnnotations;

namespace PetConsult.Models
{
    public class Pet
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = default!;

        public DateTimeOffset? Birthday { get; set; } = null;

        public DateTimeOffset AdoptionDate { get; set; } = DateTimeOffset.UtcNow;

        public ICollection<Consult> Consults { get; set; } = new List<Consult>();
    }
}