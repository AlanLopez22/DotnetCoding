using System.Net;

namespace DotnetCoding.Core.Responses
{
    public class Response<T> : Response
        where T : class
    {
        public T? Data { get; set; }
    }
    public class Response
    {
        public ICollection<string> Errors { get; set; } = new List<string>();
        public HttpStatusCode StatusCode { get; set; }
    }
    public static class ResponseBuilder
    {
        public static Response Create(HttpStatusCode statusCode)
        {
            return new Response
            {
                StatusCode = statusCode,
            };
        }
        public static Response Create(HttpStatusCode statusCode, string message)
        {
            return new Response
            {
                StatusCode = statusCode,
                Errors = new List<string> { message }
            };
        }
        public static Response Create(HttpStatusCode statusCode, IEnumerable<string> messages)
        {
            return new Response
            {
                StatusCode = statusCode,
                Errors = new List<string>(messages),
            };
        }
        public static Response<T> Create<T>(HttpStatusCode statusCode, T data)
            where T : class
        {
            return new Response<T>
            {
                StatusCode = statusCode,
                Data = data
            };
        }
        public static Response<T> Create<T>(HttpStatusCode statusCode, string message, T data)
            where T : class
        {
            return new Response<T>
            {
                StatusCode = statusCode,
                Data = data,
                Errors = new List<string> { message }
            };
        }
        public static Response<T> Create<T>(HttpStatusCode statusCode, IEnumerable<string> messages, T data)
            where T : class
        {
            return new Response<T>
            {
                StatusCode = statusCode,
                Data = data,
                Errors = new List<string>(messages),
            };
        }
    }
}
