using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalTicket.TicketManagement.Application.Models.Mail;

namespace GlobalTicket.TicketManagement.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email eamil);
    }
}
