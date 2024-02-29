using System.ComponentModel.DataAnnotations;

namespace Store99.Dto.Shoe
{
    public class CreateShoeDto
    {
        [Required(ErrorMessage = "Shoe name needs to be provided")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Brand Id needs to be provided")]
        public int BrandId { get; set; }
        [Range(0, 1, ErrorMessage = "IsInStock must be true or false")]
        public bool IsInStock { get; set; }
        //[Required(ErrorMessage = "At least one file must be provided")]
        //public ICollection<IFormFile> Files { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, BrandId: {BrandId}, IsInStock: {IsInStock}";
        }
    }
}
