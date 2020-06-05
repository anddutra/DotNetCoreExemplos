using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DotNetCoreExemplos.Configure
{
    //Configuração do Swagger na Api
    //https://docs.microsoft.com/pt-br/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-3.1
    public static class ConfigureSwagger
    {
        public static void AddSwaggerGenApi(this IServiceCollection services)
        {
            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Version = "v1", Title = "DotNetCoreExemplos", Description = "Exemplos DotNetCore" });
                setup.CustomSchemaIds((type) => type.FullName);

                var dir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                var assemblies = AppDomain.CurrentDomain.GetAssemblies().Select(assembly => assembly.GetName().Name).Where(assembly => assembly.StartsWith("DotNetCore", StringComparison.OrdinalIgnoreCase));
                foreach (var assembly in assemblies)
                {
                    var xml = Path.Combine(dir, $"{assembly}.xml");
                    if (File.Exists(xml))
                    {
                        setup.IncludeXmlComments(xml);
                    }
                }
            });
        }

        public static void UseSwaggerApi(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Docs");
                c.RoutePrefix = string.Empty;
                c.ConfigObject.AdditionalItems.Add("tagsSorter", "alpha");
                c.ConfigObject.AdditionalItems.Add("operationsSorter", "method");
                c.DefaultModelExpandDepth(2);
                c.DefaultModelRendering(ModelRendering.Model);
                c.DefaultModelsExpandDepth(1);
                c.DisplayRequestDuration();
                c.DocExpansion(DocExpansion.None);
                c.EnableDeepLinking();
                c.EnableFilter();
                c.ShowExtensions();
            });
        }
    }
}
