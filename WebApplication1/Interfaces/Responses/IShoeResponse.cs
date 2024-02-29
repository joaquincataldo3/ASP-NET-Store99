using Store99.Dto.Sho;
using System.Net;

namespace Store99.Interfaces.Responses
{
    public interface IShoeResponse
    {
        public bool Success { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }
}
