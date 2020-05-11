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
             AddSingleton - Criado/Instanciado quando a api � executada e todas as solicita��es utilizam a mesma inst�ncia.
             AddScoped    - Criado/Instanciado uma vez por solicita��o/request.
             AddTransient - Criado/Instanciado cada vez que � solicitado/injetado na classe
            */

            services.AddControllers();
            services.AddSingleton<ValueService>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddScoped<UserService>();
            services.AddScoped<ApiRequestService>();

            services.AddHealthChecksApi(); //Verifica��o de sa�de da Api.
            services.AddSwaggerGenApi(); //Configura��o Swagger.

            services.AddHostedService<HelloWorldHostedService>(); //Configurado tarefa que ir� rodar em segundo plano.
            services.AddHostedService<StartApiHostedService>(); //Configurado tarefa que ir� rodar quando a Api subir.

            //Configura o httpClient que ser� utilizado na Api.
            services.AddHttpClient("HttpClientApi", c =>
            {
                c.BaseAddress = new Uri("http://worldtimeapi.org/api/timezone/");
                c.DefaultRequestHeaders.Add("X-TraceId", "123456");
            }).AddHeaderPropagation();

            //Propaga o header do request de entrada para o request de sa�da.
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

            //app.UseMiddleware<TokenValidation>(); //Middleware para valida��o do 'token' da requisi��o.
            app.UseHealthChecksApi(); //Endpoint para verificar a saude da Api.
            app.UseSwaggerApi(); //Endpoint Swagger.
            app.UseHeaderPropagation(); //Configura a propaga��o dos headers

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}