using Store99.Dto.Sho;

namespace Store99.Interfaces.Responses
{
    public interface IOneShoeResponse: IShoeResponse
    {
        public ShoeDto? Data { get; set; }
    }
}
