using Azure;
using Microsoft.AspNetCore.Http;
using Fiker.Domain.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Fiker.Presentation.MiddleWare
{
    public class GlobalErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {

            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                context.Response.StatusCode = 500;
                var response = new BaseResponse<string>()
                {
                    Data = e.GetType().Name,
                    Message = e.Message,
                    StatusCode = ExceptionStatusCode(e)
                };
                var json = JsonSerializer.Serialize(response);
                context.Response.ContentType = "application/json";
                context?.Response.WriteAsync(json);
            }
        }
        private HttpStatusCode ExceptionStatusCode(Exception ex)
        {
            var exceptionType = ex.GetType();

            if (exceptionType == typeof(UnauthorizedAccessException))
            {
                return HttpStatusCode.Unauthorized;
            }
            else
            {
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}
