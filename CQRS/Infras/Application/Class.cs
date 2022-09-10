using System.Net;

namespace CQRS.Infras.Application
{
    public class AppException:Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public AppException(HttpStatusCode statusCode,string message):base(message)
        {
            StatusCode = statusCode;
        }
    }
}
