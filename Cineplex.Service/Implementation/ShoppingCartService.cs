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
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<EmailMessage> _mailRepository;
        private readonly IRepository<TicketinOrder> _ticketInOrderRepository;
        private readonly UserRepoInterface _userRepository;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, UserRepoInterface userRepository, IRepository<EmailMessage> mailRepository, IRepository<Order> orderRepository, IRepository<TicketinOrder> ticketInOrderRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _ticketInOrderRepository = ticketInOrderRepository;
            _mailRepository = mailRepository;
        }


        public bool deleteTicketsFromSoppingCart(string userId, Guid ticketId)
        {
            if (!string.IsNullOrEmpty(userId) && ticketId != null)
            {
                var loggedInUser = this._userRepository.GetById(userId);

                var userShoppingCart = loggedInUser.ShoppingCart;

                var itemToDelete = userShoppingCart.TicketsInShoppingCart.Where(z => z.ticketId.Equals(ticketId)).FirstOrDefault();

                userShoppingCart.TicketsInShoppingCart.Remove(itemToDelete);

                this._shoppingCartRepository.Update(userShoppingCart);

                return true;
            }
            return false;
        }

        public ShoppingCartDTO getShoppingCartInfo(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var loggedInUser = this._userRepository.GetById(userId);

                var userCard = loggedInUser.ShoppingCart;

                var allTickets = userCard.TicketsInShoppingCart.ToList();

                var allTicketsPrices = allTickets.Select(z => new
                {
                    TicketPrice = z.ticket.Price,
                    Quantity = z.Quantity
                }).ToList();

                double totalPrice = 0.0;

                foreach (var item in allTicketsPrices)
                {
                    totalPrice += item.Quantity * item.TicketPrice;
                }

                var reuslt = new ShoppingCartDTO
                {
                    Tickets = allTickets,
                    TotalPrice = totalPrice
                };

                return reuslt;
            }
            return new ShoppingCartDTO();
        }

        public bool order(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var loggedInUser = this._userRepository.GetById(userId);
                var userCard = loggedInUser.ShoppingCart;

                EmailMessage mail = new EmailMessage();
                mail.MailTo = loggedInUser.Email;
                mail.Subject = "Sucessfuly created order!";
                mail.Status = false;


                Order order = new Order
                {
                    User = loggedInUser,
                    UserId = userId
                };

                this._orderRepository.Insert(order);

                List<TicketinOrder> ticketinOrders = new List<TicketinOrder>();

                var result = userCard.TicketsInShoppingCart.Select(z => new TicketinOrder
                {
                    TicketId = z.ticket.id,
                    Ticket = z.ticket,
                    OrderId = order.id,
                    Order = order,
                    Quantity = z.Quantity
                }).ToList();

                StringBuilder sb = new StringBuilder();

                var totalPrice = 0.0;

                sb.AppendLine("Your order is completed. The order conatins: ");

                for (int i = 1; i <= result.Count(); i++)
                {
                    var currentItem = result[i - 1];
                    totalPrice += currentItem.Quantity * currentItem.Ticket.Price;
                    sb.AppendLine(i.ToString() + ". " + currentItem.Ticket.id + " with quantity of: " + currentItem.Quantity + " and price of: $" + currentItem.Ticket.Price);
                }

                sb.AppendLine("Total price for your order: " + totalPrice.ToString());

                mail.Content = sb.ToString();


                ticketinOrders.AddRange(result);

                foreach (var item in ticketinOrders)
                {
                    this._ticketInOrderRepository.Insert(item);
                }

                loggedInUser.ShoppingCart.TicketsInShoppingCart.Clear();

                this._userRepository.Update(loggedInUser);
                this._mailRepository.Insert(mail);

                return true;
            }

            return false;
        }
    }
}

