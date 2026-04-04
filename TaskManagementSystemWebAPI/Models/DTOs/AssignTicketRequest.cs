namespace TaskManagementSystemWebAPI.Models.DTOs
{
    public class AssignTicketRequest
    {
        public int TicketId { get; set; }
        public int AgentId { get; set; }
    }
}
