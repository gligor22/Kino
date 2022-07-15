using Cineplex.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cineplex.Domain.DTO
{
    public class ShoppingCartDTO
    {

        public List<TicketsInShoppingCart> Tickets { get; set; }

        public double TotalPrice { get; set; }

    }
}
