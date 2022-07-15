using Cineplex.Domain.Domain;
using Cineplex.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cineplex.Service.Interface
{
    public interface ITicketService
    {
        List<Ticket> getAllTickets();
        Ticket GetDetailsForTickets(Guid? id);
        void CreateNewTicket(Ticket t);
        void UpdateTicket(Ticket t);
        TicketShoppingCartDTO GetShoppingCartInfo(Guid? id);
        void DeleteTickeet(Guid id);
        bool AddToShoppingCart(TicketShoppingCartDTO item, string userID);
    }
}
