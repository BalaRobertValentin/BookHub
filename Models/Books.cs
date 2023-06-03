using System.ComponentModel.DataAnnotations;

namespace BookRental.Models
{
    public class Books : BaseEntity
    {
        [Required]
        [StringLength(40, MinimumLength = 1)]
        public string? Title { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string? Author { get; set; }

        [Required]
        [StringLength(10000, MinimumLength = 1)]
        public string? Description { get; set; }

        public string? CoverImageUrl { get; set; }

        [Required]
        public Genre Genre { get; set; } 

        public float Price { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string? Publisher { get; set; }

        [Required]
        [Range(1900, 2023)]
        public int Year { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Pages { get; set; }

        [Required]
        public Language Language { get; set; }

    }
    public enum Language
    {
        English,
        Spanish,
        French,
        German,
        Chinese,
        Japanese,
        Romanian
    }
    public enum Genre
    {
        [Display(Name = "Action And Adventure")]
        ActionAndAdventure,
        Biography,
        Childrens,
        Classics,
        [Display(Name = "Comic And Graphic Novels")]
        ComicAndGraphicNovels,
        [Display(Name = "Crime And Mystery")]
        CrimeAndMystery,
        Drama,
        Erotica,
        Fantasy,
        [Display(Name = "Historical Fiction")]
        HistoricalFiction,
        Horror,
        Humor,
        LGBTQ,
        [Display(Name = "Literary Fiction")]
        LiteraryFiction,
        [Display(Name = "Non-Fiction")]
        NonFiction,
        Poetry,
        Romance,
        [Display(Name = "Science Fiction")]
        ScienceFiction,
        [Display(Name = "Short Stories")]
        ShortStories,
        [Display(Name = "Thriller And Suspense")]
        ThrillerAndSuspense,
        Travel,
        [Display(Name = "Young Adult")]
        YoungAdult
    }
}
