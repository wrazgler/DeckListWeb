using System;

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
using DeckListWeb.Repository.Models;
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
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddScoped<IContextOptions>(contextOptions =>
                new ContextOptions
                {
                    ConnectionString = connectionString
                });
            var dbProvider = new DbProvider(Configuration);
            var currentSQL = dbProvider.GetSQL(connectionString);
            switch (currentSQL)
            {
                case DbProviderState.PostgeSQL:
                    services.AddScoped<IRepositoryContextFactory, PostreSQLContextFactory>();
                    break;
                case DbProviderState.MSSQL:
                    services.AddScoped<IRepositoryContextFactory, MSSQLContextFactory>();
                    break;
                default:
                    throw new Exception("SQl doesn't choose");
            }
            services.AddScoped<IBaseRepository>(provider =>
                new BaseRepository(connectionString,
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
