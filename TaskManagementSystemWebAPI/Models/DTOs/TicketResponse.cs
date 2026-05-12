using TaskManagementSystemWebAPI.Models.Entities;

namespace TaskManagementSystemWebAPI.Models.DTOs
{
    public class TicketResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedByName { get; set; }
        public string AssignedToName { get; set; }
    }
}
