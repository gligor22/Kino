using Cineplex.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cineplex.Service.Interface
{
    public interface IActhorsService
    {
        List<Acthor> getAll();
        Acthor GetDetails(Guid? id);
        void CreateNew(Acthor t);
        void Update(Acthor t);
        void Delete(Guid id);
    }
}
