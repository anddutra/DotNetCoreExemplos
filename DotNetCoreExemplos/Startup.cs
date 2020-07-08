using DotNetCoreExemplos.Configure;
using DotNetCoreExemplos.Exceptions;
using DotNetCoreExemplos.Repository;
using DotNetCoreExemplos.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace DotNetCoreExemplos
{
    //Boas práticas
    //https://docs.microsoft.com/pt-br/dotnet/architecture/modern-web-apps-azure/architectural-principles
    //https://docs.microsoft.com/pt-br/aspnet/core/performance/performance-best-practices?view=aspnetcore-3.1
    //Classe Startup
    //https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/startup?view=aspnetcore-3.1
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            /*
             https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1
             https://medium.com/@nelson.souza/net-core-dependency-injection-1c1900d1bef
             https://medium.com/volosoft/asp-net-core-dependency-injection-best-practices-tips-tricks-c6e9c67f9d96

             AddSingleton - Criado/Instanciado quando a api é executada e todas os requests utilizam a mesma instância.
             AddScoped    - Criado/Instanciado uma vez por solicitação/request.
             AddTransient - Criado/Instanciado cada vez que é solicitado/injetado na classe
            */

            services.AddControllers();

            services.AddSingleton<ValueService>();
            services.AddScoped<UserService>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<CryptoRijndaelService>();
            services.AddScoped<LinqService>();

            services.AddHealthChecksApi(); //Verificação de saúde da Api.
            services.AddSwaggerGenApi(); //Configuração Swagger.
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHealthChecksApi(); //Endpoint da saude da Api.
            app.UseSwaggerApi(); //Endpoint do Swagger.

            app.UseMiddleware<HttpResponseExceptionMiddleware>(); //Middleware para captuar as exeções e retornar a msg

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}