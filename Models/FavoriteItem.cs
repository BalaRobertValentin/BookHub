namespace BookRental.Models
{
    public class FavoriteItem
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public Books Book { get; set; }
        public int BookId { get; set; }
    }
}
