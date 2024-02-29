using System.ComponentModel.DataAnnotations;

namespace Store99.Dto.ShoeFile
{
    public class ShoeFileDto
    {
        public int Id { get; set; }
        public string Secure_Url { get; set; }
        public string Public_Id { get; set; }
    }
}
