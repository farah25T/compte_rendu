using compte_rendu.Models;

namespace compte_rendu.Repositories.RepositoryContracts
{
    public interface IMovieRepository
    {
      
            List<Movie> GetAllMovies();
            Movie GetMovieById(int id);
            void CreateMovie(Movie movie);
            void EditMovie(Movie movie);
            void DeleteMovie(int id);
            List<Movie> GetMoviesByGenre(int genreId);
            List<Movie> GetAllMoviesOrderedAscending();
            List<Movie> GetMoviesByUserDefinedGenre(string userDefinedGenre);

        }
    }

