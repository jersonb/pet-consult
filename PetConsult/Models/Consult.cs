using System.ComponentModel.DataAnnotations;

namespace PetConsult.Models
{
    public class Consult
    {
        [Key]
        public int Id { get; set; }

        public DateTimeOffset Date { get; set; } = DateTimeOffset.UtcNow;
        public string Veterinarian { get; set; } = default!;
        public string Procedments { get; set; } = default!;
        public string Medicines { get; set; } = default!;

        public Pet Pet { get; set; } = default!;
        public int? PetId { get; set; }
    }
}