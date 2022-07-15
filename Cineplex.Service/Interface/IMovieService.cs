using Cineplex.Domain.Domain;
using Cineplex.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cineplex.Service.Interface
{
    public interface IMovieService
    {
        List<Movie> getAll();
        Movie GetDetails(Guid? id);
        void CreateNew(MovieDTO t);
        void Update(MovieDTO t);
        void Delete(Guid id);
    }
}
