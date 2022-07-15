using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cineplex.Domain.Domain
{
    public class TicketinOrder : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public int Quantity { get; set; }
    }
}
