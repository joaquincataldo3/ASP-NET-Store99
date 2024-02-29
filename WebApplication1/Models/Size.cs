using System.ComponentModel.DataAnnotations;

namespace Store99.Models
{
    public class Size
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Number { get; set; }
        public ICollection<StockShoeSize> StockShoesSizes { get; set; }

    }
}
