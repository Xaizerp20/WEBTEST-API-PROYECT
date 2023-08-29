using System.Net;

namespace WEBTEST_API_PROYECT.Models
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public bool isSucessfull { get; set; } = true;

        public  List<string> ErrorMessage { get; set; }

        public object Result { get; set; }


    }
}
