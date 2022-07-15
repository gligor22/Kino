using Cineplex.Domain;
using Cineplex.Domain.Domain;
using Cineplex.Domain.Identity;
using Cineplex.Repository.Interface;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cineplex.Repository
{
    public class ApplicationDbContext : IdentityDbContext<CineplexApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Acthor> Acthors { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<ActhorInMovies> ActhorInMovies { get; set; }
        public virtual DbSet<TicketinOrder> TicketinOrders { get; set; }
        public virtual DbSet<TicketsInShoppingCart> TicketsInShoppingCarts { get; set; }

       

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Acthor>().Property(z => z.id).ValueGeneratedOnAdd();
            builder.Entity<Movie>().Property(z => z.id).ValueGeneratedOnAdd();
            builder.Entity<Order>().Property(z => z.id).ValueGeneratedOnAdd();
            builder.Entity<ShoppingCart>().Property(z => z.id).ValueGeneratedOnAdd();
            builder.Entity<Ticket>().Property(z => z.id).ValueGeneratedOnAdd();

            builder.Entity<TicketinOrder>().HasKey(z => new { z.OrderId, z.TicketId });
            builder.Entity<TicketsInShoppingCart>().HasKey(z => new { z.shoppingCartId, z.ticketId });
            builder.Entity<ActhorInMovies>().HasKey(z => new { z.ActhorID, z.MovieID });

            builder.Entity<ShoppingCart>().HasOne(z => z.Owner).WithOne(z => z.ShoppingCart).HasForeignKey<ShoppingCart>(z => z.OwnerId);
            builder.Entity<Order>().HasOne(z => z.User).WithOne(z => z.Order).HasForeignKey<Order>(z => z.UserId);
            builder.Entity<Ticket>().HasOne(z => z.Movie).WithOne(z => z.Ticket).HasForeignKey<Ticket>(z => z.MovieID);

            builder.Entity<TicketinOrder>().HasOne(z => z.Ticket).WithMany(z => z.ticketsInOrder).HasForeignKey(z => z.OrderId);
            builder.Entity<TicketinOrder>().HasOne(z => z.Order).WithMany(z => z.ticketsInOrder).HasForeignKey(z => z.TicketId);

            builder.Entity<ActhorInMovies>().HasOne(z => z.Achtor).WithMany(z => z.Movies).HasForeignKey(z => z.MovieID);
            builder.Entity<ActhorInMovies>().HasOne(z => z.Movie).WithMany(z => z.Acthors).HasForeignKey(z => z.ActhorID);

            builder.Entity<TicketsInShoppingCart>().HasOne(z => z.ticket).WithMany(z => z.ticketsInShoppingCart).HasForeignKey(z => z.shoppingCartId);
            builder.Entity<TicketsInShoppingCart>().HasOne(z => z.shoppingCart).WithMany(z => z.TicketsInShoppingCart).HasForeignKey(z => z.ticketId);

           
            
        }
    }
}
