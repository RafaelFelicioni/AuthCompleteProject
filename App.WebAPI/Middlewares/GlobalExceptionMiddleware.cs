using CleanArchMonolit.Shared.Responses;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Text.Json;

namespace CleanArchMonolit.Shared.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next,
                                         ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var endpoint = context.GetEndpoint();

                string controllerName = "Unknown";
                string actionName = "Unknown";

                if (endpoint != null)
                {
                    var adc = endpoint.Metadata
                                      .GetMetadata<ControllerActionDescriptor>();
                    if (adc != null)
                    {
                        controllerName = adc.ControllerName;
                        actionName = adc.ActionName;
                    }
                }

                _logger.LogError(
                    ex,
                    "Unhandled exception in {Controller}.{Action}: {Message}",
                    controllerName,
                    actionName,
                    ex.Message
                );
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                var result = Result<object>.Fail("Ocorreu um erro interno, por favor tente novamente, caso o erro persista, entre em contato com o suporte.");
                await context.Response.WriteAsync(JsonSerializer.Serialize(result));
            }
        }
    }
}
