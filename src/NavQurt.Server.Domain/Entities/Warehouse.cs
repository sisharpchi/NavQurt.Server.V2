namespace NavQurt.Server.Domain.Entities
{
    public class Warehouse
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
