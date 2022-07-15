using Cineplex.Domain;
using Cineplex.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cineplex.Repository.Interface
{
    public interface IOrderRepository
    {
        public List<Order> getAllOrders();
        public Order getOrderDetails(BaseEntity model);


    }
}
