using TaskManagementSystemWebAPI.Models.Entities;

namespace TaskManagementSystemWebAPI.Repositories.Interfaces
{
    public interface ITicketRepository
    {
        Task AddAsync(Ticket ticket);
        Task<List<Ticket>> GetByUserIdAsync(int userId);
        Task<Ticket> GetByIdAsync(int id);
        Task UpdateAsync(Ticket ticket);
    }
}
