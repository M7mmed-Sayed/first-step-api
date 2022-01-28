using my_books.Data.ViewModel;
using System.Net;

namespace my_books.Exceptions
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomExceptionMiddleware(RequestDelegate next)
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
                await HandleExceptionAsync(context,ex);
            }

        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            var response = new ErrorVM()
            {
                StatusCode = context.Response.StatusCode,
                Message = " internal Server Error From Custom Middleware",
                Path = "path go there"
            };
            return context.Response.WriteAsync(response.ToString());
        }
    }
}
