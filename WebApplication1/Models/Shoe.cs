using System.ComponentModel.DataAnnotations;

namespace Store99.Models
{
    public class Shoe
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        public bool IsInStock { get; set; }
        [Required]
        public int BrandId { get; set; }

        // here we have the one to many reference
        public Brand Brand { get; set; }

        // here we have the many to many reference
        public ICollection<StockShoeSize> StockShoesSizes { get; set; }
        // public ICollection<ShoeFile> ShoeFile { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, BrandId: {BrandId}, IsInStock: {IsInStock}";
        }

    }
}
