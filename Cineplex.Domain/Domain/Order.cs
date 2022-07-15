using Cineplex.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cineplex.Domain.Domain
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public virtual CineplexApplicationUser User { get; set; }
        public virtual ICollection<TicketinOrder> ticketsInOrder { get; set; }
    }
}
