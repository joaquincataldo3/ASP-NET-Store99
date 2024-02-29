using Store99.Dto.Sho;
using Store99.Interfaces;
using System.Net;

namespace Store99.Service
{
    public class ShoeServiceResponse: IShoeServiceResponse
    {
        public bool Success { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string? Message { get; set; }
        public ShoeDto? Shoe { get; set; }
    }
}
