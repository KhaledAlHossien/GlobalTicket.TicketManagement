using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalTicket.TicketManagement.Application.Contracts.Persistence;
using GlobalTicket.TicketManagement.Domain.Entities;

namespace GlobalTicket.TicketManagement.Persistence.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(GloboTicketDbContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> IsEventNameAndDateUnique(string name , DateTime eventDate)
        {
            var matches = _dbContext.Events.Any(e => e.Name.Equals(name) &&
            e.Date.Date.Equals(eventDate));
            return Task.FromResult(matches);
        }
    }
}
