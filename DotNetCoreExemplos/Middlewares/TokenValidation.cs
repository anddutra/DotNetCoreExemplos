using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreExemplos.Middlewares
{
    //Toda requisição feita para esta API passa por este Middleware de validação.
    //Caso no cabeçalho da requisição não tenha um 'token' a requisição será negada e será retornado o código 401 (Unauthorized)
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
            if (!String.IsNullOrEmpty(context.Request.Headers["token"]))
                await _next(context);
            else
                context.Response.StatusCode = (int) System.Net.HttpStatusCode.Unauthorized;
        }
    }
}
