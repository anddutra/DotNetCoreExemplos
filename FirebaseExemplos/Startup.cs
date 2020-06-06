using FireBaseExemplos.Models;
using FireBaseExemplos.Repository;
using FireBaseExemplos.Services;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Concurrent;

namespace FireBaseExemplos
{
    //https://firebase.google.com/docs/admin/setup?hl=pt-br#c_2
    //https://firebase.google.com/docs/firestore/quickstart#c
    //https://firebase.google.com/docs/firestore/query-data/listen
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

            services
                .AddSingleton(_ => FirestoreDb.Create("apidotnetcore"))
                .AddScoped<UserService>()
                .AddSingleton<IUserRepository, UserRepository>()
                .AddSingleton<ConcurrentDictionary<string, UserFirebase>>()
                .AddSingleton<UserListenerService>()
                .AddHostedService<FirestoreListenerService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
