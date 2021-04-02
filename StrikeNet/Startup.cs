using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StrikeNet.Helpers;
using StrikeNet.BusinessLogic.Extensions;
using StrikeNet.EntityFramework.DbContext;
using StrikeNet.EntityFramework.Entities;
using StrikeNet.EntityFramework.DbContext.ContextFactories;
using StrikeNet.BusinessLogic.Services.Interface;
using StrikeNet.BusinessLogic.Services;
using StrikeNet.EntityFramework.Repositories;
using StrikeNet.EntityFramework.Repositories.Interface;
using System;
using Microsoft.Data.SqlClient;

namespace StrikeNet
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            //if (env.IsDevelopment()) builder.AddUserSecrets<Startup>();

            Configuration = builder.Build();

            HostingEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostingEnvironment { get; }

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

            //app.UseStatusCodePagesWithRedirects("~/Account/AccessDenied");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContexts<StrikeNetDbContext>(Configuration);

            services.AddIdentity<UserIdentity, RoleIdentity>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<StrikeNetDbContext>();

            services.AddStrikeNetServices<StrikeNetDbContext>();

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IIdentityRepository, IdentityRepository>();

            services.AddScoped<IPredictionService, PredictionService>();
            services.AddScoped<IPredictionRepository, PredictionRepository>();

            services.AddRazorPages();
            services.AddMvc();

            services.AddControllersWithViews();
        }
    }
}

