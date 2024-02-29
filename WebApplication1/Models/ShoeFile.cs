using System.ComponentModel.DataAnnotations;

namespace Store99.Models
{
    public class ShoeFile
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Secure_Url { get; set; }
        [Required]
        public string Public_Id { get; set; }
        public int ShoeId { get; set; }
        public Shoe Shoe { get; set; }

    }
}
