using TaskManagementSystemWebAPI.Models.Entities;

namespace TaskManagementSystemWebAPI.Models.DTOs
{
    public class TicketResponse
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public required string CreatedByName { get; set; }
        public required string AssignedToName { get; set; }
    }
}
