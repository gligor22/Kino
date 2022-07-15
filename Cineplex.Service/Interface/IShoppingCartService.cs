using Cineplex.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cineplex.Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDTO getShoppingCartInfo(string userId);
        bool deleteTicketsFromSoppingCart(string userId, Guid productId);
        bool order(string userId);
    }
}
