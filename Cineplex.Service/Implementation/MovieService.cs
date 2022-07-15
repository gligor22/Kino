using Cineplex.Domain.Domain;
using Cineplex.Domain.DTO;
using Cineplex.Repository.Interface;
using Cineplex.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cineplex.Service.Implementation
{
    public class MovieService : IMovieService
    {

        private readonly IRepository<Movie> _movieRepository;
        private readonly IRepository<ActhorInMovies> _acthorInMoviesRepository;

        public MovieService(IRepository<Movie> movieRepository, IRepository<ActhorInMovies> achtorInMoviesRepository)
        {
            _movieRepository = movieRepository;
            _acthorInMoviesRepository = achtorInMoviesRepository;
        }

        public void CreateNew(MovieDTO t)
        {
            Movie k = new Movie
            {
                Title = t.Title,
                Duration = t.Duration,
                movieType = t.movieType,
                id = t.id,
            };

            foreach (Acthor a in t.Acthors)
            {
                ActhorInMovies item = new ActhorInMovies
                {
                    Movie = k,
                    MovieID = k.id,
                    Achtor = a,
                    ActhorID = a.id
                };
                _acthorInMoviesRepository.Insert(item);
            }

            _movieRepository.Insert(k);
        }

        public void Delete(Guid id)
        {
            _movieRepository.Delete(_movieRepository.Get(id));
        }

        public List<Movie> getAll()
        {
           return _movieRepository.GetAll().ToList();
        }

        public Movie GetDetails(Guid? id)
        {
            return _movieRepository.Get(id);
        }

        public void Update(MovieDTO t)
        {
            Movie k = new Movie
            {
                Title = t.Title,
                Duration = t.Duration,
                movieType = t.movieType,
                id = t.id,
            };

            foreach (Acthor a in t.Acthors)
            {
                ActhorInMovies item = new ActhorInMovies
                {
                    Movie = k,
                    MovieID = k.id,
                    Achtor = a,
                    ActhorID = a.id
                };
                _acthorInMoviesRepository.Update(item);
            }

            _movieRepository.Update(k);
        }
    }
}
