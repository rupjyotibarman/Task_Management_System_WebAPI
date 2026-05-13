namespace TaskManagementSystemWebAPI.Models.DTOs
{
    public class CreateTicketRequest
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
    }
}
