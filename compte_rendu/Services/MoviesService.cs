using compte_rendu.Models;
using compte_rendu.Repositories.RepositoryContracts;
using compte_rendu.Services.ServicesContracts;

namespace compte_rendu.Services
{
   
        public class MoviesService : IMoviesService
        {
            private readonly    IMoviesService _movieRepository;

            public MoviesService(IMoviesService movieRepository)
            {
                _movieRepository = movieRepository;
            }

            public List<Movie> GetAllMovies()
            {
                return _movieRepository.GetAllMovies();
            }

            public Movie GetMovieById(int id)
            {
                return _movieRepository.GetMovieById(id);
            }

            public void CreateMovie(Movie movie)
            {
                _movieRepository.CreateMovie(movie);
            }

            public void Edit(Movie movie)
            {
                _movieRepository.Edit(movie);
            }

            public void Delete(int id)
            {
                _movieRepository.Delete(id);
            }

            public List<Movie> GetMoviesByGenre(int genreId)
            {
                return _movieRepository.GetMoviesByGenre(genreId);
            }

            public List<Movie> GetAllMoviesOrderedAscending()
            {
                return _movieRepository.GetAllMoviesOrderedAscending();
            }
            public List<Movie> GetMoviesByUserDefinedGenre(string userDefinedGenre)
            {
                return _movieRepository.GetMoviesByUserDefinedGenre(userDefinedGenre);
            }
        }
    }

