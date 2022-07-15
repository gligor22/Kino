using Cineplex.Domain;
using Cineplex.Domain.Domain;
using Cineplex.Domain.DTO;
using Cineplex.Repository.Interface;
using Cineplex.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cineplex.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly UserRepoInterface _userRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository, UserRepoInterface userRepository)
        {
            _orderRepository = orderRepository;
            this._userRepository = userRepository;
        }

        public List<Order> getall()
        {
            return this._orderRepository.getAllOrders();
        }

        public Order getOrderDetails(BaseEntity model)
        {
            return this._orderRepository.getOrderDetails(model);

        }

         public OrderDTO getOrderInfo(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var loggedInUser = this._userRepository.GetByIdOrder(userId);

                var order = loggedInUser.Order;

                var allTickets = order.ticketsInOrder.ToList();

                var allTicketsPrices = allTickets.Select(z => new
                {
                    TicketPrice = z.Ticket.Price,
                    Quantity = z.Quantity
                }).ToList();

                double totalPrice = 0.0;

                foreach (var item in allTicketsPrices)
                {
                    totalPrice += item.Quantity * item.TicketPrice;
                }

                var reuslt = new OrderDTO
                {
                    Tickets = allTickets,
                    TotalPrice = totalPrice
                };
                return reuslt;
            }
            return new OrderDTO();
        }

        public List<Order> getOrderList(string userId)
        {
            var loggedInUser = this._userRepository.GetByIdOrder(userId);
            List<Order> orderList = _orderRepository.getAllOrders();

            List<Order> selectedList = (List<Order>)orderList.Where(z => z.UserId.Equals(loggedInUser));

            return selectedList;
        }
    }
}
