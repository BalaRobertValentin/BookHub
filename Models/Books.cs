namespace BookRental.Models
{
    public class Books : BaseEntity
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Description { get; set; }
        public string? CoverImageUrl { get; set; }
        public string? Genre { get; set; } 
        public float RentalPrice { get; set; }
        public string? Publisher { get; set; }
        public int Year { get; set; }
        public int Pages { get; set; }
        public string? Language { get; set; }
    }
}
