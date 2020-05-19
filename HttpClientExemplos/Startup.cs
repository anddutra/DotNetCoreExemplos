using System;
using HttpClientExemplos.Middlewares;
using HttpClientExemplos.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HttpClientExemplos
{
    //https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/http-requests?view=aspnetcore-3.1
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<WorldTimeApiRequestService>();
            services.AddSingleton<FireBaseExemplosRequestService>();

            //Configura o httpClient para chamada da Api WorldTime - http://worldtimeapi.org/
            services.AddHttpClient("WorldTime", c =>
            {
                c.BaseAddress = new Uri("http://worldtimeapi.org/api/timezone/");
                c.DefaultRequestHeaders.Add("X-TraceId", "123456");
            });

            //Configura o httpClient para chamada da Api FireBaseExemplos
            services.AddHttpClient("FireBaseExemplos", c =>
            {
                c.BaseAddress = new Uri("http://localhost:7000/api/FireBase/");
                c.DefaultRequestHeaders.Add("X-TraceId", "123456");
            }).AddHeaderPropagation();

            //Propaga o header do request de entrada para o request de saída.
            services.AddHeaderPropagation(options =>
            {
                options.Headers.Add("token");
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<TokenValidation>(); //Middleware para validação do 'token' da requisição.
            app.UseHeaderPropagation(); //Configura a propagação dos headers

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
