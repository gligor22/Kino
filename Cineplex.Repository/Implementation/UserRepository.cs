using Cineplex.Domain.Identity;
using Cineplex.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cineplex.Repository.Implementation
{
    public class UserRepository : UserRepoInterface
    {
        private readonly ApplicationDbContext context;
        private DbSet<CineplexApplicationUser> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<CineplexApplicationUser>();
        }
        public IEnumerable<CineplexApplicationUser> GetAll()
        {
            return entities
                .Include(z => z.ShoppingCart)
                .Include("ShoppingCart.TicketsInShoppingCart")
                .Include("ShoppingCart.TicketsInShoppingCart.ticket")
                .AsEnumerable();
        }

        public CineplexApplicationUser GetById(string id)
        {
            return entities
               .Include(z => z.ShoppingCart)
               .Include("ShoppingCart.TicketsInShoppingCart")
               .Include("ShoppingCart.TicketsInShoppingCart.ticket")
               .Include("ShoppingCart.TicketsInShoppingCart.ticket.Movie")
               .SingleOrDefault(s => s.Id == id);
        }
        public CineplexApplicationUser GetByIdOrder(string id)
        {
            return entities
               .Include(z => z.Order)
               .Include("Order.TicketsInOrder")
               .Include("Order.TicketsInOrder.Ticket")
               .Include("Order.TicketsInOrder.Ticket.Movie")
               .SingleOrDefault(s => s.Id == id);
        }
        public void Insert(CineplexApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(CineplexApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(CineplexApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
    }
}
