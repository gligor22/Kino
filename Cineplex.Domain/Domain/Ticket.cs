using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Cineplex.Domain.Domain
{
    public class Ticket : BaseEntity
    {
        [Required]
        public double Price { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public DateTime ValidTo { get; set; }
        [Required]
        public Guid MovieID { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual ICollection<TicketsInShoppingCart> ticketsInShoppingCart { get; set; }
        public virtual ICollection<TicketinOrder> ticketsInOrder { get; set; }
    }
}
