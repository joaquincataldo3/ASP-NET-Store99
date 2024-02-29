using System.ComponentModel.DataAnnotations;

namespace Store99.Models
{
    public class StockShoeSize
    {
        [Required]
        public int ShoeId { get; set; }
        // referencia one
        public Shoe Shoe { get; set; }
        [Required]
        public int SizeId { get; set; }
        // referencia one
        public Size SizeNumber { get; set; }    
    }
}
