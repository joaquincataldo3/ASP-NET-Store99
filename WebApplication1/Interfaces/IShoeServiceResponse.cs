using Store99.Dto.Sho;
using System.Net;

namespace Store99.Interfaces
{
    public class IShoeServiceResponse
    {
        public bool Success { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public ShoeDto Shoe { get; set; }
    }
}
