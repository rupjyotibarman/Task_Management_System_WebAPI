using TaskManagementSystemWebAPI.Models.DTOs;
using TaskManagementSystemWebAPI.Models.Entities;
namespace TaskManagementSystemWebAPI.Services.Interfaces
{
    public interface ITicketService
    {
        Task<Ticket> CreateTicketAsync(int userId, CreateTicketRequest requestDto);
        Task<List<Ticket>> GetMyTicketsAsync(int userId);
        Task AssignTicketAsync(int ticketId, int agentId);
        Task UpdateStatusAsync(int ticketId, int userId, string status);
        Task<List<Ticket>> GetAssignedTicketsAsync(int userId);
        Task<List<Ticket>> GetAllTicketsAsync();
        Task DeleteTicketAsync(int ticketId, int userId);
    }
}
