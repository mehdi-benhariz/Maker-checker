using System.Net;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json.Linq;

namespace maker_checker_v1.Middleware
{
    public class ErrorHandler
    {
        private readonly RequestDelegate _next;

        public ErrorHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                // Response.ContentType = "application/json";

                switch (error)
                {
                    case ApplicationException e:
                        // custom application error
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                //CONVERT json string to json object

                JObject json = JObject.Parse(error.Message);

                var errors = JsonSerializer.Serialize(new { errors = json });
                await context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(errors));

                // return context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(errors));
                // return context.Response.WriteAsync(errors);

            }
        }
    }
}