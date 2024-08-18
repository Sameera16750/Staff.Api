using System.Net;

namespace Staff.Application.Models.Response.Common
{
    public class IdResponse<T>
    {
        public T id { get; set; }
        public string Message { get; set; }
    }
    
    public class MessageResponse
    {
        public string Message { get; set; } 
    }

    public class ListResponse<T>
    {
        public List<T> List { get; set; }
        public int Total { get; set; }
    }
    
    public class ResponseWithCode<T>
    {
        public T? Response { get; set; }
        public HttpStatusCode StatusCode { get; set; } 
    }
    
}