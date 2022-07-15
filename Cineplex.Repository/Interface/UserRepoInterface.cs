using Cineplex.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cineplex.Repository.Interface
{
    public interface UserRepoInterface
    {
        IEnumerable<CineplexApplicationUser> GetAll();
        CineplexApplicationUser GetById(string id);
        CineplexApplicationUser GetByIdOrder(string id);
        void Insert(CineplexApplicationUser user);
        void Update(CineplexApplicationUser user);
        void Delete(CineplexApplicationUser user);
    }
}
