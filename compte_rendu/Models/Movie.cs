using System.ComponentModel.DataAnnotations.Schema;

namespace compte_rendu.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? GenreId { get; set; }

        public Genre? Genre { get; set; }
        public string? ImagePath { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public ICollection<Customer>? Customers { get; set; }
    }
}
