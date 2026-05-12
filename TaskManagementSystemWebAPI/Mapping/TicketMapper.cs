using TaskManagementSystemWebAPI.Enums;
using TaskManagementSystemWebAPI.Models.DTOs;
using TaskManagementSystemWebAPI.Models.Entities;

namespace TaskManagementSystemWebAPI.Mapping
{
    public static class TicketMapper
    {
        // Entity → Response DTO
        public static TicketResponse ToResponse(Ticket ticket)
        {
            return new TicketResponse
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                Status = ticket.Status,
                CreatedAt = ticket.CreatedAt,

                CreatedByName = ticket.CreatedByUser != null
                    ? ticket.CreatedByUser.Name
                    : null,

                AssignedToName = ticket.AssignedToUser != null
                    ? ticket.AssignedToUser.Name
                    : null
            };
        }

        // List<Entity> → List<DTO>
        public static List<TicketResponse> ToResponseList(List<Ticket> tickets)
        {
            return tickets.Select(ToResponse).ToList();
        }

        // Create DTO → Entity
        public static Ticket ToEntity(CreateTicketRequest dto, int userId)
        {
            return new Ticket
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = TicketStatus.Open.ToString(),
                CreatedBy = userId,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}