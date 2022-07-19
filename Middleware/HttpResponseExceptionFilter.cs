using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace maker_checker_v1.Middleware
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception == null)
                return;

            if (context.Exception is HttpResponseException httpResponseException)
            {
                context.Result = new ObjectResult(httpResponseException.Value)
                {
                    StatusCode = httpResponseException.StatusCode
                };


            }
            if (context.Exception is System.Exception ex)
            {
                //create a json named errors 

                string title = ex.Message.Split("|")[0];
                string[] content = new string[] { ex.Message.Split("|")[1] };
                var json = "{\" " + title + "\":" + JsonConvert.SerializeObject(content) + "}";
                context.Result = new ObjectResult(JsonConvert.SerializeObject(new { errors = JObject.Parse(json) }))
                {
                    StatusCode = 400
                };
                // StatusCode =ex.

            }
            context.ExceptionHandled = true;
        }
    }
}