using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestAppRichDomain.Core.Interfaces;
using TestAppRichDomain.Core.Services;
using TestAppRichDomain.Infrastructure;
using TestAppRichDomain.Repository;
using TestAppRichDomain.Shared;
using TestAppRichDomain.UI.Helpers;
using TestAppRichDomain.UI.Interfaces;
using TestAppRichDomain.UI.Services;

namespace TestAppRichDomain.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connString;
            try
            {
                string password = PasswordDecr.Dcryptpassword(Configuration["PathKey"], Configuration["PasswordKey"]);
                connString = Configuration["Connection"] + password;
            }
            catch
            {
                connString = Configuration.GetConnectionString("Default");
            }
            services.AddControllersWithViews();
            //Fluent Migrator
            services.AddLogging(c => c.AddFluentMigratorConsole())
            .AddFluentMigratorCore()
            .ConfigureRunner(c => c
            .AddSqlServer()
            .WithGlobalConnectionString(connString)
            .ScanIn(Assembly.GetExecutingAssembly()).For.All());
            //Db
            services.AddDbContext<SiteContext>(options => options.UseSqlServer(connString,
                    x => x.MigrationsAssembly("TestAppRichDomain.Infrastructure")));
            //Identity
            services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<SiteContext>();
            //Services
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IBasketViewModelService, BasketViewModelService>();
            services.AddScoped<IOrderViewModelService, OrderViewModelService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddTransient<ISaveImage, SaveFile>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); 
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            if (env.EnvironmentName != "Test")
            {
                app.Migrate();
            }
        }
    }
}
