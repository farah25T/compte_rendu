using System.ComponentModel.DataAnnotations;

namespace compte_rendu.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [StringLength(50, MinimumLength = 5, ErrorMessage = "the name must be between 5 and 50 caracters")]
        public string Name { get; set; }

        public int? MemberShipId { get; set; }
        public Membershiptypes? MemberShipType { get; set; }
        public ICollection<Movie>? Movies { get; set; }
    }
}
