using System.Net;

namespace Alza.CustomResponse
{
    public class ResponseHandler
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public Object? Data { get; set; }
    }
}
