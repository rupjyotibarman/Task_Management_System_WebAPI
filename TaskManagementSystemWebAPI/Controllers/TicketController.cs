using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystemWebAPI.Data;
using TaskManagementSystemWebAPI.Models.DTOs;
using TaskManagementSystemWebAPI.Models.Entities;

namespace TaskManagementSystemWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TicketController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TicketController(AppDbContext context)
        {
            _context = context;
        }

        //1. Create Ticket (User)
        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] CreateTicketRequest dto)
        {
            var userId = int.Parse(User.FindFirst("userId").Value);

            var ticket = new Ticket
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = "Open",
                CreatedBy = userId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return Ok(ticket);
        }

        //2. Get My Tickets (User)
        [HttpGet("my")]
        public async Task<IActionResult> GetMyTickets()
        {
            var userId = int.Parse(User.FindFirst("userId").Value);

            var tickets = await _context.Tickets
                .Where(t => t.CreatedBy == userId)
                .ToListAsync();

            return Ok(tickets);
        }

        //3. Get Assigned Tickets (Agent)
        [Authorize(Roles = "Agent")]
        [HttpGet("assigned")]
        public async Task<IActionResult> GetAssignedTickets()
        {
            var userId = int.Parse(User.FindFirst("userId").Value);

            var tickets = await _context.Tickets
                .Where(t => t.AssignedTo == userId)
                .ToListAsync();

            return Ok(tickets);
        }

        //4. Get All Tickets (Admin)
        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllTickets()
        {
            var tickets = await _context.Tickets
                .Include(t => t.CreatedByUser)
                .Include(t => t.AssignedToUser)
                .ToListAsync();

            return Ok(tickets);
        }

        //5. Assign Ticket (Admin)
        [Authorize(Roles = "Admin")]
        [HttpPost("assign")]
        public async Task<IActionResult> AssignTicket([FromBody] AssignTicketRequest dto)
        {
            var ticket = await _context.Tickets.FindAsync(dto.TicketId);

            if (ticket == null)
                return NotFound("Ticket not found");

            var agent = await _context.Users.FindAsync(dto.AgentId);

            if (agent == null)
                return NotFound("Agent not found");

            ticket.AssignedTo = dto.AgentId;
            ticket.Status = "InProgress";

            await _context.SaveChangesAsync();

            return Ok(ticket);
        }

        //6. Update Ticket Status (Agent)
        [Authorize(Roles = "Agent")]
        [HttpPost("status")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateStatusRequest dto)
        {
            var userId = int.Parse(User.FindFirst("userId").Value);

            var ticket = await _context.Tickets.FindAsync(dto.TicketId);

            if (ticket == null)
                return NotFound("Ticket not found");

            if (ticket.AssignedTo != userId)
                return Forbid("You are not assigned to this ticket");

            ticket.Status = dto.Status;

            await _context.SaveChangesAsync();

            return Ok(ticket);
        }

        //7. Delete Ticket (Optional - Owner Only)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var userId = int.Parse(User.FindFirst("userId").Value);

            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
                return NotFound("Ticket not found");

            if (ticket.CreatedBy != userId)
                return Forbid("You can only delete your own tickets");

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return Ok("Ticket deleted");
        }
    }
}