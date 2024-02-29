using Store99.Interfaces.Responses;
using System.Net;

namespace Store99.Service.ShoeService
{
    public class ShoeResponse: IShoeResponse
    {
        public bool Success { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }
}
