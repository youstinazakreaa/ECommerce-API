using Azure;
using Microsoft.Extensions.Logging;
using onineproject.Errors;
using System;
using System.Net;
using System.Text.Json;

namespace onineproject.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment environment;

        public ExceptionMiddleware(RequestDelegate Next, ILogger<ExceptionMiddleware> Logger ,IHostEnvironment Environment)
        {
         
            next = Next;
            logger = Logger;
            environment = Environment;
        }
        public async Task InvokeAsync (HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }catch(Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = environment.IsDevelopment() ?
                    new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace?.ToString())
                    :new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
                var JsonResponse = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(JsonResponse);
            }
        

        }
    }
}
