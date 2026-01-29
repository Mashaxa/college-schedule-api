using System.Net;
using System.Text.Json;

namespace CollegeSchedule.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware>
       logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context) //метод, пытается выполнить запрос. если в сервисе или контроллере происходит ошибка, управление переходит в блок catch.
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleException(context, ex);
            }
        }
        private static Task HandleException(HttpContext context, Exception ex) //метод анализирует тип ошибки с помощью конструкции switch
        {
            var status = ex switch
            {
                ArgumentOutOfRangeException => HttpStatusCode.BadRequest,
                ArgumentException => HttpStatusCode.BadRequest, //400
                KeyNotFoundException => HttpStatusCode.NotFound, //404
                _ => HttpStatusCode.InternalServerError
            };
            var response = new
            {
                error = ex.Message
            };
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        //чтобы Middleware начал перехватывать ошибки, его необходимо подключить в конвейер обработки запросов в файле Program.cs
    }
}