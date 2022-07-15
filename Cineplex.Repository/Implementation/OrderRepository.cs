using Cineplex.Domain;
using Cineplex.Domain.Domain;
using Cineplex.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cineplex.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Order> entities;
        string errorMessage = string.Empty;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Order>();
        }
        public List<Order> getAllOrders()
        {
            return entities
                .Include(z => z.User)
                .Include(z => z.ticketsInOrder)
                .Include("ticketsInOrder.Ticket")
                .Include("ticketsInOrder.Ticket.Movie")
                .ToListAsync().Result;
        }

        public Order getOrderDetails(BaseEntity model)
        {
            return entities
               .Include(z => z.User)
                .Include(z => z.ticketsInOrder)
                .Include("ticketsInOrder.Ticket")
                .Include("ticketsInOrder.Ticket.Movie")
               .SingleOrDefaultAsync(z => z.id == model.id).Result;
        }

    }
}
