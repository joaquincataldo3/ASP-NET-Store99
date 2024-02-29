using Store99.Dto.Brand;
using Store99.Dto.ShoeFile;
using Store99.Models;
using System.ComponentModel.DataAnnotations;

namespace Store99.Dto.Sho
{
    public class ShoeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsInStock { get; set; }

        // here we have the one to many reference
        public BrandDto Brand { get; set; }
        public ICollection<ShoeFileDto> ShoeFiles { get; set; }

        // here we have the many to many reference
        public ICollection<StockShoeSize> StockShoesSizes { get; set; }
    }
}
