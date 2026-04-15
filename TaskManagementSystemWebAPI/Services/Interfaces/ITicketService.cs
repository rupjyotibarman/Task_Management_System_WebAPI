using TaskManagementSystemWebAPI.Models.DTOs;
using TaskManagementSystemWebAPI.Models.Entities;
namespace TaskManagementSystemWebAPI.Services.Interfaces
{
    public interface ITicketService
    {
        Task<Ticket> CreateTicketAsync(int userId, CreateTicketRequest requestDto);
        Task<List<Ticket>> GetMyTicketAsync(int userId);
        Task AssignTicketAsync(int ticketId, int agentId);
        Task UpdateStatusAsync(int ticketId, int userId, string status);
    }
}
