using Store99.Dto.Sho;
using Store99.Interfaces.Responses;
using System.Net;

namespace Store99.Service.ShoeService
{
    public class ShoeCollectionResponse: ShoeResponse, ICollectionShoeResponse
    {
        public ICollection<ShoeDto>? Data { get; set; }  
    }
}
