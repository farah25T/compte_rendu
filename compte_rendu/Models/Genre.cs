namespace compte_rendu.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string? GenreName { get; set; }
        public ICollection<Movie>? Movies { get; set; }
    }
}
