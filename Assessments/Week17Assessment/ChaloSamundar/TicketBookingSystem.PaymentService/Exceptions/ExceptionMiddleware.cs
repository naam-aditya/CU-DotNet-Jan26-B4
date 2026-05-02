using System.Net;
using System.Text.Json;

namespace TicketBookingSystem.PaymentService.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        //public async Task Invoke(HttpContext context)
        //{
        //    try
        //    {
        //        await _next(context);
        //    }
        //    catch (Exception ex)
        //    {
        //        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        //        context.Response.ContentType = "application/json";

        //        var response = new
        //        {
        //            StatusCode = context.Response.StatusCode,
        //            Message = ex.Message
        //        };

        //        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        //    }
        //}
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                await WriteResponse(context, 404, ex.Message);
            }
            catch (BadRequestException ex)
            {
                await WriteResponse(context, 400, ex.Message);
            }
            catch (PaymentFailedException ex)
            {
                await WriteResponse(context, 422, ex.Message);
            }
            catch (Exception ex)
            {
                await WriteResponse(context, 500, ex.Message);
            }
        }

        private static async Task WriteResponse(HttpContext context, int statusCode, string message)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                StatusCode = statusCode,
                Message = message
            }));
        }
    }
}
