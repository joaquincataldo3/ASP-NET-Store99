using Store99.Dto.Sho;

namespace Store99.Interfaces.Responses
{
    public interface ICollectionShoeResponse
    {
        public ICollection<ShoeDto>? Data { get; set; }
    }
}
