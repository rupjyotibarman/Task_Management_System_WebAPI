using Microsoft.EntityFrameworkCore;
using TaskManagementSystemWebAPI.Data;
using TaskManagementSystemWebAPI.Models.Entities;
using TaskManagementSystemWebAPI.Repositories.Interfaces;

namespace TaskManagementSystemWebAPI.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AppDbContext _context;

        public TicketRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Ticket>> GetByUserIdAsync(int userId)
        {
            return await _context.Tickets
                .Where(t => t.CreatedBy == userId)
                .ToListAsync();
        }

        public async Task<Ticket> GetByIdAsync(int id)
        {
            return await _context.Tickets.FindAsync(id);
        }

        public async Task UpdateAsync(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
        }
    }
}
