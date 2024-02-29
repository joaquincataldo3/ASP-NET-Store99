using System.ComponentModel.DataAnnotations;

namespace Store99.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        // here we have the many to one reference
        public ICollection<Shoe> Shoes { get; set; }
        
    }
}
