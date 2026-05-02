using System.Net;
using System.Text.Json;
using FluentValidation;

namespace TicketBookingSystem.TripService.Exceptions
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            int statusCode;
            object response;

            switch (exception)
            {
                case NotFoundException:
                    statusCode = (int)HttpStatusCode.NotFound;
                    response = new { message = exception.Message, statusCode };
                    break;

                case BadRequestException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    response = new { message = exception.Message, statusCode };
                    break;

                // FluentValidation ValidationException — group errors by property
                case ValidationException validationEx:
                    statusCode = (int)HttpStatusCode.BadRequest;

                    var errors = validationEx.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(e => e.ErrorMessage).ToArray()
                        );

                    response = new
                    {
                        message = "Validation failed",
                        statusCode,
                        errors
                    };
                    break;

                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    response = new { message = "Internal Server Error", statusCode };
                    break;
            }

            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
