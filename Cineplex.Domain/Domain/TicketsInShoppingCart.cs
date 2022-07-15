using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cineplex.Domain.Domain
{
    public class TicketsInShoppingCart : BaseEntity
    {
        public Guid ticketId { get; set; }
        public Guid shoppingCartId { get; set; }
        public Ticket ticket { get; set; }
        public ShoppingCart shoppingCart { get; set; }
        public int Quantity { get; set; }
    }
}
