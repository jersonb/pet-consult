namespace PetConsult.ViewObjects.Requests
{
    public class PetUpdate
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty!;

        public DateTimeOffset? Birthday { get; set; } = null;

        public DateTimeOffset AdoptionDate { get; set; } = DateTimeOffset.UtcNow;
    }
}