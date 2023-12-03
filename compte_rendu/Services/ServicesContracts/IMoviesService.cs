using compte_rendu.Models;

namespace compte_rendu.Services.ServicesContracts
{
    public interface IMoviesService
    {
        List<Movie> GetAllMovies();
        Movie GetMovieById(int id);
        void CreateMovie(Movie movie);
        void Edit(Movie movie);
        void Delete(int id);

        List<Movie> GetMoviesByGenre(int genreId);
        List<Movie> GetAllMoviesOrderedAscending();
        List<Movie> GetMoviesByUserDefinedGenre(string userDefinedGenre);
    }
}
