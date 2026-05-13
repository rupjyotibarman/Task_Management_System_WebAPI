namespace TaskManagementSystemWebAPI.Models.Entities
{
    public class Role
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        // Navigation
        public required ICollection<User> Users { get; set; }
    }
}
