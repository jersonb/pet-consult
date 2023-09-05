namespace PetConsult.ViewObjects
{
    public class PetResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public DateTimeOffset? Birthday { get; set; } = null;

        public DateTimeOffset AdoptionDate { get; set; } = DateTimeOffset.UtcNow;
    }
}