namespace TaskManagementSystemWebAPI.Models.Entities
{
    public class User
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Email { get; set; }

        public required string PasswordHash { get; set; }

        // Foreign Key
        public int RoleId { get; set; }

        // Navigation Property
        public required Role Role { get; set; }

        // Navigation for Tickets
        public required ICollection<Ticket> CreatedTickets { get; set; }

        public required ICollection<Ticket> AssignedTickets { get; set; }
    }
}
