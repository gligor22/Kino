using Cineplex.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cineplex.Domain.DTO
{
    public class OrderDTO
    {
        public List<TicketinOrder> Tickets { get; set; }

        public double TotalPrice { get; set; }
    }
}
