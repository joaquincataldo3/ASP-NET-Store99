using Store99.Dto.Sho;
using Store99.Dto.Shoe;
using Store99.Interfaces.Responses;

namespace Store99.Interfaces.Services
{
    public interface IShoeService
    {
        public ICollectionShoeResponse ValidateGetOnDemandShoes();
        public ICollectionShoeResponse ValidateGetInStockShoes();
        public IOneShoeResponse ValidateShoeCreation(CreateShoeDto createShoeDto);
        public IOneShoeResponse ValidateGetShoe(int shoeId);

    }
}
