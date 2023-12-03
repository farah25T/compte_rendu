namespace compte_rendu.Models
{
    public class Membershiptypes
    {
        public Guid Id { get; set; }
        public float SignUpFee { get; set; }
        public int DurationInMonth { get; set; }
        public float DiscountRate { get; set; }

        public string Name { get; set; }
        public ICollection<Customer>? Customers { get; set; }
    }
}
