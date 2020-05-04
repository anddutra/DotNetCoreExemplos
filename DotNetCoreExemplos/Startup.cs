using DotNetCoreExemplos.Middlewares;
using DotNetCoreExemplos.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            services.AddControllers();
            services.AddScoped<ValueServices>(); //Criado/Instanciado uma vez por solicita��o.
            services.AddSingleton<UserServices>(); //Criado/Instanciado quando a api � executada e todas as solicita��es utilizam a mesma inst�ncia.
            services.AddHealthChecksApi(); //Verifica��o de sa�de da Api.
            services.AddSwaggerGenApi(); //Configura��o Swagger
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseMiddleware<TokenValidation>(); //Middleware para valida��o do 'token' da requisi��o.
            app.UseHealthChecksApi(); //Endpoint para verificar a saude da Api.
            app.UseSwaggerApi(); //Endpoint Swagger

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
