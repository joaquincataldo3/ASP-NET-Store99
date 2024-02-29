using Store99.Dto.Shoe;

namespace Store99.Interfaces
{
    public interface IShoeService
    {
        public IShoeServiceResponse ValidateShoeCreation(CreateShoeDto createShoeDto);
    }
}
