namespace PetConsult.ViewObjects.Requests
{
    public class PetCreate
    {
        public string Name { get; set; } = string.Empty!;

        public DateTimeOffset? Birthday { get; set; } = null;

        public DateTimeOffset AdoptionDate { get; set; } = DateTimeOffset.UtcNow;
    }
}