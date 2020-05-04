using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Reflection;

namespace DotNetCoreExemplos.Middlewares
{
    //Configuração do Swagger na Api
    //https://docs.microsoft.com/pt-br/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-3.1
    public static class ConfigureSwagger
    {
        public static void AddSwaggerGenApi(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Version = "v1", Title = "DotNetCoreExemplos", Description = "Exemplos DotNetCore" });
                c.CustomSchemaIds((type) => type.FullName);

                var xmlFile = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), $"{Assembly.GetEntryAssembly().GetName().Name}.xml");

                if (File.Exists(xmlFile))
                {
                    c.IncludeXmlComments(xmlFile);
                }

                if (File.Exists(xmlFile.Replace(".Server.xml", ".Common.xml")))
                {
                    c.IncludeXmlComments(xmlFile.Replace(".Server.xml", ".Common.xml"));
                }

                if (File.Exists(xmlFile.Replace(".Server.xml", ".xml")))
                {
                    c.IncludeXmlComments(xmlFile.Replace(".Server.xml", ".xml"));
                }
            });
        }

        public static void UseSwaggerApi(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Docs");
            });
        }
    }
}
