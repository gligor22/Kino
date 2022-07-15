using Cineplex.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cineplex.Domain.DTO
{
    public class TicketShoppingCartDTO
    {
        public Ticket ticket { get; set; }
        public Guid ticketID { get; set; }
        public int quantity { get; set; }
    }
}