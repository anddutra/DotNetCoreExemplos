using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Linq;
using System.Reflection;

namespace DotNetCoreExemplos.Configure
{
    //Verifica a saúde da Api. 
    //https://docs.microsoft.com/pt-br/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-3.1
    public static class ConfigureHealthChecks
    {
        //HealthChecks que serão verificados.
        //Nesse caso foi adicionado apenas um exemplo.
        public static void AddHealthChecksApi(this IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddCheck("Example", () =>
                    HealthCheckResult.Healthy("Example is OK!"), tags: new[] { "example" });
        }

        //Endpoint para verificar a saude da Api.
        public static void UseHealthChecksApi(this IApplicationBuilder app)
        {
            
            app.UseHealthChecks($"/health", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = async (context, report) =>
                {
                    var result = System.Text.Json.JsonSerializer.Serialize(
                        new
                        {
                            application = Assembly.GetEntryAssembly().GetName().Name,
                            status = report.Status.ToString(),
                            healthChecks = report.Entries.Select(e => new
                            {
                                check = e.Key,
                                status = Enum.GetName(typeof(HealthStatus), e.Value.Status),
                                description = e.Value.Description
                            }),
                        });
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(result);
                }
            });
        }
    }
}
