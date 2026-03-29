namespace TaskManagementSystemWebAPI.Models.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        // Foreign Key
        public int RoleId { get; set; }

        // Navigation Property
        public Role Role { get; set; }

        // Navigation for Tickets
        public ICollection<Ticket> CreatedTickets { get; set; }

        public ICollection<Ticket> AssignedTickets { get; set; }
    }
}
