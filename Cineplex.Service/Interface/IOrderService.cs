using Cineplex.Domain;
using Cineplex.Domain.Domain;
using Cineplex.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cineplex.Service.Interface
{
    public interface IOrderService
    {
        public List<Order> getall();

        public Order getOrderDetails(BaseEntity model);
        public OrderDTO getOrderInfo(string userId);

        public List<Order> getOrderList(string userId);
    }
}
