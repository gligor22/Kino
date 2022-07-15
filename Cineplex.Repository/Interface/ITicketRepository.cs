using Cineplex.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cineplex.Repository.Interface
{
    public interface ITicketRepository
    {
        IEnumerable<Ticket> GetAll();
        Ticket Get(Guid? id);
        void Insert(Ticket entity);
        void Update(Ticket entity);
        void Delete(Ticket entity);
    }
}
