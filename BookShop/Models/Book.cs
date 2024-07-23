using System.ComponentModel.DataAnnotations;

namespace BookShop.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [MaxLength(length:100)]
        public string Description { get; set; }
        public string Language { get; set; }
        [Required]
        [MaxLength(length:13)]
        public string ISBN { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DatePunished { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public int  Price { get; set; }
        [Required]
        public string Author { get; set; }
        [Display(Name ="Imge URL")]
        public string ImgUrl { get; set; }
    }
}
