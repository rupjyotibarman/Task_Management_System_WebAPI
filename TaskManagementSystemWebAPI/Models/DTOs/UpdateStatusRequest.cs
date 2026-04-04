namespace TaskManagementSystemWebAPI.Models.DTOs
{
    public class UpdateStatusRequest
    {
        public int TicketId { get; set; }
        public string Status { get; set; }
    }
}
