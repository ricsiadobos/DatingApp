using API.Errors;
using System.Net;
using System.Text.Json;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next { get; }
        private ILogger<ExceptionMiddleware> _logger { get; }
        private IHostEnvironment _env { get; }

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);               //naplózzuk a hibát

                context.Response.ContentType = "application/json";  //átadjuk a hiba tartalmát
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;  // status kódnak, hogy 500 server hiba

                var response = _env.IsDevelopment()                                     // megvizsgáljuk, hogy fejleszői módban vagyunk-e?
                    ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())   //feljleszői módban részltetezzük a keretkezett hibát
                    : new ApiException(context.Response.StatusCode, ex.Message, "Internal Server Error");   // production módban szerver hiba üzenetet jelenítünk meg

                var options = new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase };  //mivel itt nem automatizált a szerializálás ezért használunk JSON serializer-t

                var json = JsonSerializer.Serialize(response, options); //JSON serializálás

                await context.Response.WriteAsync(json);  //visszatérés a serializálst adattal

            }
        }

    }
}
