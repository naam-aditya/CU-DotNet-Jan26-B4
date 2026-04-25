using FluentValidation;
using Serilog;
using System.Text.Json;
using TicketBookingSystem.ItineraryService.Common;
using TicketBookingSystem.ItineraryService.Exceptions;

namespace TicketBookingSystem.ItineraryService.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            ApiResponse response;
            int statusCode;

            switch (exception)
            {
                case ValidationException validationEx:
                    statusCode = StatusCodes.Status400BadRequest;
                    var errors = validationEx.Errors
                        .Select(e => $"{e.PropertyName}: {e.ErrorMessage}")
                        .ToList();
                    response = ApiResponse.FailureResponse(
                        "Validation failed.",
                        statusCode,
                        errors
                    );
                    break;

                case ItineraryException itineraryEx:
                    statusCode = itineraryEx.StatusCode;
                    response = ApiResponse.FailureResponse(
                        itineraryEx.Message,
                        statusCode
                    );
                    break;

                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    response = ApiResponse.FailureResponse(
                        "An internal server error occurred.",
                        statusCode
                    );
                    break;
            }

            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
