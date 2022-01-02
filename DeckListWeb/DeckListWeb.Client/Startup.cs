using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using DeckListWeb.Model.Interfaces;
using DeckListWeb.Model.Services;

using DeckListWeb.Repository;
using DeckListWeb.Repository.Factories;
using DeckListWeb.Repository.Interfaces;
using DeckListWeb.Repository.Repositories;

namespace DeckListWeb.Client
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
            //var connection = Configuration.GetConnectionString("MSSQLConnection");
            var connection = Configuration.GetConnectionString("PostgreSQLConnection");

            var isPostgreSql = connection == Configuration.GetConnectionString("PostgreSQLConnection");

            services.AddScoped<IContextOptions>(contextOptions =>
                new ContextOptions
                {
                    ConnectionString = connection,
                    IsPostgreSql = isPostgreSql
                });

            services.AddScoped<IRepositoryContextFactory, RepositoryContextFactory>();
            services.AddScoped<IBaseRepository>(provider =>
                new BaseRepository(connection, isPostgreSql,
                    provider.GetService<IRepositoryContextFactory>()));
            services.AddScoped<IDeckListService, DeckListService>();
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=GetAllDecks}/{id?}");
            });
        }
    }
}
