using Cineplex.Domain.Domain;
using Cineplex.Repository.Interface;
using Cineplex.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cineplex.Service.Implementation
{
    public class AchtorsService : IActhorsService
    {

        private readonly IRepository<Acthor> _achtorRepository;

        public AchtorsService(IRepository<Acthor> achtorRepository)
        {
            _achtorRepository = achtorRepository;
        }
        public void CreateNew(Acthor t)
        {
            _achtorRepository.Insert(t);
        }

        public void Delete(Guid id)
        {
            _achtorRepository.Delete(_achtorRepository.Get(id));
        }

        public List<Acthor> getAll()
        {
            return _achtorRepository.GetAll().ToList();
        }

        public Acthor GetDetails(Guid? id)
        {
            return _achtorRepository.Get(id);
        }

        public void Update(Acthor t)
        {
            _achtorRepository.Update(t);
        }
    }
}
