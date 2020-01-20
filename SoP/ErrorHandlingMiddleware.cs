using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Data.Common;
using System.Net;
using System.Threading.Tasks;

namespace SoP_Data.Helpers
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            var result = JsonConvert.SerializeObject(
                new { 
                    errorCode = "",
                    error = ex.Message 
                }
            );
            if (ex is DbException)
            {
                result = JsonConvert.SerializeObject(
                    new {
                        errorCode = "DB.Exception",
                        error = ex.Message
                    }
                );
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
