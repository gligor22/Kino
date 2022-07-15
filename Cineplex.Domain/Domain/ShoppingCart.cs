using Cineplex.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cineplex.Domain.Domain
{
    public class ShoppingCart : BaseEntity
    {
        public string OwnerId { get; set; }
        public virtual CineplexApplicationUser Owner { get; set; }
        public virtual ICollection<TicketsInShoppingCart> TicketsInShoppingCart { get; set; }
    }
}
