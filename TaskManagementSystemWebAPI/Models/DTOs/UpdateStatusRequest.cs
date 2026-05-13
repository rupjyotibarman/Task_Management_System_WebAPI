namespace TaskManagementSystemWebAPI.Models.DTOs
{
    public class UpdateStatusRequest
    {
        public int TicketId { get; set; }
        public required string Status { get; set; }
    }
}
