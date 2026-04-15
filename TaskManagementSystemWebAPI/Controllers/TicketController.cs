using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystemWebAPI.Models.DTOs;
using TaskManagementSystemWebAPI.Services.Interfaces;

namespace TaskManagementSystemWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        // 1️ Create Ticket (User)
        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] CreateTicketRequest dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var ticket = await _ticketService.CreateTicketAsync(userId, dto);

            return Ok(ticket);
        }

        // 2️ Get My Tickets (User)
        [HttpGet("my")]
        public async Task<IActionResult> GetMyTickets()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var tickets = await _ticketService.GetMyTicketsAsync(userId);

            return Ok(tickets);
        }

        // 3️ Get Assigned Tickets (Agent)
        [Authorize(Roles = "Agent")]
        [HttpGet("assigned")]
        public async Task<IActionResult> GetAssignedTickets()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var tickets = await _ticketService.GetAssignedTicketsAsync(userId);

            return Ok(tickets);
        }

        // 4️ Get All Tickets (Admin)
        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllTickets()
        {
            var tickets = await _ticketService.GetAllTicketsAsync();

            return Ok(tickets);
        }

        // 5️ Assign Ticket (Admin)
        [Authorize(Roles = "Admin")]
        [HttpPost("assign")]
        public async Task<IActionResult> AssignTicket([FromBody] AssignTicketRequest dto)
        {
            await _ticketService.AssignTicketAsync(dto.TicketId, dto.AgentId);

            return Ok("Ticket assigned successfully");
        }

        // 6️ Update Ticket Status (Agent)
        [Authorize(Roles = "Agent")]
        [HttpPost("status")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateStatusRequest dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            await _ticketService.UpdateStatusAsync(dto.TicketId, userId, dto.Status);

            return Ok("Status updated successfully");
        }

        // 7️ Delete Ticket (Owner Only)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            await _ticketService.DeleteTicketAsync(id, userId);

            return Ok("Ticket deleted successfully");
        }
    }
}