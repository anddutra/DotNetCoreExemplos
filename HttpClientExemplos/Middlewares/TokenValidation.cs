using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace HttpClientExemplos.Middlewares
{
    //Toda requisição feita para esta API passa por este Middleware de validação.
    //Caso a requisição não tenha um 'token' no header, ela será negada 
    //e será retornado o código 401 (Unauthorized)
    //https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/middleware/?view=aspnetcore-3.1
    public class TokenValidation
    {
        private RequestDelegate _next { get; set; }

        public TokenValidation(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!string.IsNullOrEmpty(context.Request.Headers["token"]))
                await _next(context);
            else
            {
                context.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Não foi informado o token da requisição");
            }
        }
    }
}
