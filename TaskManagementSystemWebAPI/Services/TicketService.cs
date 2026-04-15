using TaskManagementSystemWebAPI.Models.DTOs;
using TaskManagementSystemWebAPI.Models.Entities;
using TaskManagementSystemWebAPI.Services.Interfaces;
using TaskManagementSystemWebAPI.Repositories.Interfaces;
using TaskManagementSystemWebAPI.Enums;

namespace TaskManagementSystemWebAPI.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        //Ticket Handles Methods
        public async Task<Ticket> CreateTicketAsync(int userId, CreateTicketRequest dto)
        {
            var ticket = new Ticket
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = TicketStatus.Open.ToString(),
                CreatedBy = userId,
                CreatedAt = DateTime.UtcNow
            };

            await _ticketRepository.AddAsync(ticket);
            return ticket;
        }

        public async Task<List<Ticket>> GetMyTicketsAsync(int userId)
        {
            return await _ticketRepository.GetByUserIdAsync(userId);
        }

        public async Task AssignTicketAsync(int ticketId, int agentId)
        {
            var ticket = await _ticketRepository.GetByIdAsync(ticketId);

            if (ticket == null)
                throw new Exception("Ticket not found");

            ticket.AssignedTo = agentId;
            ticket.Status = TicketStatus.InProgress.ToString();

            await _ticketRepository.UpdateAsync(ticket);
        }

        public async Task UpdateStatusAsync(int ticketId, int userId, string status)
        {
            var ticket = await _ticketRepository.GetByIdAsync(ticketId);

            if (ticket == null)
                throw new Exception("Ticket not found");

            if (ticket.AssignedTo != userId)
                throw new Exception("Unauthorized");

            ticket.Status = status;

            await _ticketRepository.UpdateAsync(ticket);
        }
        public async Task<List<Ticket>> GetAssignedTicketsAsync(int userId)
        {
            return await _ticketRepository.GetAssignedTicketsAsync(userId);
        }
        public async Task<List<Ticket>> GetAllTicketsAsync()
        {
            return await _ticketRepository.GetAllAsync();
        }
        public async Task DeleteTicketAsync(int ticketId, int userId)
        {
            var ticket = await _ticketRepository.GetByIdAsync(ticketId);

            if (ticket == null)
                throw new Exception("Ticket not found");

            if (ticket.CreatedBy != userId)
                throw new Exception("You can only delete your own tickets");

            await _ticketRepository.DeleteAsync(ticket);
        }
    }
}
