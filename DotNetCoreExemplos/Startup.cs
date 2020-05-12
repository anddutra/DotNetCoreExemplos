using DotNetCoreExemplos.HostedServices;
using DotNetCoreExemplos.Middlewares;
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
    //Tutoriais e Dicas
    //https://www.tutorialsteacher.com/core
    //https://docs.microsoft.com/pt-br/dotnet/architecture/modern-web-apps-azure/architectural-principles
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
             https://andrewlock.net/the-difference-between-getservice-and-getrquiredservice-in-asp-net-core/

             AddSingleton - Criado/Instanciado quando a api é executada e todas as solicitações utilizam a mesma instância.
             AddScoped    - Criado/Instanciado uma vez por solicitação/request.
             AddTransient - Criado/Instanciado cada vez que é solicitado/injetado na classe
            */

            services.AddControllers();
            services.AddSingleton<ValueService>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddScoped<UserService>();
            services.AddScoped<ApiRequestService>();

            services.AddHealthChecksApi(); //Verificação de saúde da Api.
            services.AddSwaggerGenApi(); //Configuração Swagger.

            services.AddHostedService<HelloWorldHostedService>(); //Configurado tarefa que irá rodar em segundo plano.
            services.AddHostedService<StartApiHostedService>(); //Configurado tarefa que irá rodar quando a Api subir.

            //Configura o httpClient que será utilizado na Api.
            services.AddHttpClient("HttpClientApi", c =>
            {
                c.BaseAddress = new Uri("http://worldtimeapi.org/api/timezone/");
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

            //app.UseMiddleware<TokenValidation>(); //Middleware para validação do 'token' da requisição.
            app.UseHealthChecksApi(); //Endpoint para verificar a saude da Api.
            app.UseSwaggerApi(); //Endpoint Swagger.
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