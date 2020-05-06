using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DoggyDaycare.API.Exceptions
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                await HandleException(context, e);
            }
        }

        public static Task HandleException(HttpContext context, Exception e)
        {
            HttpStatusCode code = HttpStatusCode.InternalServerError;

            if (e is AppException)
            {
                code = HttpStatusCode.BadRequest;
            }

            string result = JsonConvert.SerializeObject(new { error = e.Message });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) code;

            return context.Response.WriteAsync(result);
        }
    }
}
