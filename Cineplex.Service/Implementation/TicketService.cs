using Cineplex.Domain.Domain;
using Cineplex.Domain.DTO;
using Cineplex.Repository.Implementation;
using Cineplex.Repository.Interface;
using Cineplex.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cineplex.Service.Implementation
{
    public class TicketService : ITicketService
    {

        private readonly ITicketRepository _ticketRepository;
        private readonly IRepository<TicketsInShoppingCart> ticketInShoppingCartRepository;
        private readonly UserRepoInterface _userRepository;

        public TicketService(ITicketRepository ticketRepository, IRepository<TicketsInShoppingCart> _ticketInShoppingCartRepository, UserRepoInterface userRepository)
        {
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
            ticketInShoppingCartRepository = _ticketInShoppingCartRepository;
        }
        public bool AddToShoppingCart(TicketShoppingCartDTO item, string userID)
        {
            var user = this._userRepository.GetById(userID);

            var userShoppingCard = user.ShoppingCart;

            if (item.ticketID != null && userShoppingCard != null)
            {
                var ticket = this.GetDetailsForTickets(item.ticketID);

                if (ticket != null)
                {
                    TicketsInShoppingCart itemToAdd = new TicketsInShoppingCart
                    {
                        ticket = ticket,
                        ticketId = ticket.id,
                        shoppingCart = userShoppingCard,
                        shoppingCartId = userShoppingCard.id,
                        Quantity = item.quantity
                    };

                    var existing = userShoppingCard.TicketsInShoppingCart.Where(z => z.shoppingCartId == userShoppingCard.id && z.ticketId == itemToAdd.ticketId).FirstOrDefault();

                    if (existing != null)
                    {
                        existing.Quantity += itemToAdd.Quantity;
                        this.ticketInShoppingCartRepository.Update(existing);

                    }
                    else
                    {
                        this.ticketInShoppingCartRepository.Insert(itemToAdd);
                    }
                    return true;
                }
                return false;
            }
            return false;
        }

        public void CreateNewTicket(Ticket t)
        {
            _ticketRepository.Insert(t);
        }

        public void DeleteTickeet(Guid id)
        {
            _ticketRepository.Delete(_ticketRepository.Get(id));
        }

        public List<Ticket> getAllTickets()
        {
            return _ticketRepository.GetAll().ToList();
        }

        public Ticket GetDetailsForTickets(Guid? id)
        {
            return this._ticketRepository.Get(id);
        }

        public TicketShoppingCartDTO GetShoppingCartInfo(Guid? id)
        {
            var ticket = this.GetDetailsForTickets(id);
            TicketShoppingCartDTO model = new TicketShoppingCartDTO
            {
                ticket = ticket,
                ticketID = ticket.id,
                quantity = 1
            };
            return model;

        }

        public void UpdateTicket(Ticket t)
        {
            _ticketRepository.Update(t);
        }
    }
}
