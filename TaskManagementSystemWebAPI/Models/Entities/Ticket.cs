namespace TaskManagementSystemWebAPI.Models.Entities
{
    public class Ticket
    {
        public int Id { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public required string Status { get; set; }

        public DateTime CreatedAt { get; set; }

        // Foreign Keys
        public int CreatedBy { get; set; }
        public int AssignedTo { get; set; }

        // Navigation Properties
        public User? CreatedByUser { get; set; }

        public User? AssignedToUser { get; set; }
    }
}
