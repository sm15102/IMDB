using Imdb.Domain.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace Imdb.api.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ConvertException(context, ex);
            }
        }

        private Task ConvertException(HttpContext context, Exception exception)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";

            var result = string.Empty;

            switch (exception)
            {
                case DomainException domainException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    result = domainException.Message;
                    break;
                case Exception ex:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    result = "Something went wrong!!!";
                    break;
            }

            if (result == string.Empty)
            {
                result = JsonConvert.SerializeObject(new { error = exception.Message });
            }

            context.Response.StatusCode = (int)httpStatusCode;
            return context.Response.WriteAsync(result);
        }
    }
}
