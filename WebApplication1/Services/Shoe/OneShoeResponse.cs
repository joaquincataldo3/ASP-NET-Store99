using Store99.Dto.Sho;
using Store99.Interfaces.Responses;
using System.Net;

namespace Store99.Service.ShoeService
{
    public class OneShoeResponse : ShoeResponse, IOneShoeResponse
    {
        public ShoeDto? Data { get; set; }
    }
}
