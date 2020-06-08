using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DotNetCoreExemplos.Exceptions
{
    //Middleware para captuar as exeções do tipo HttpResponseException e retornar a msg limpa 
    //Ex.: CustomExceptionExemplo
    public class HttpResponseExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public HttpResponseExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext).ConfigureAwait(false);
            }
            catch (HttpResponseException ex)
            {
                httpContext.Response.Clear();
                httpContext.Response.StatusCode = ex.StatusCode;
                await httpContext.Response.WriteAsync(ex.Message).ConfigureAwait(false);
            }
        }
    }
}