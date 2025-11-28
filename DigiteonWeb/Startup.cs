using DigiteonWeb.Common;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PolarCastleWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolarCastleWeb
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
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(options =>
               {
                   options.LoginPath = "/Home/Index";
                   options.AccessDeniedPath = "/Home/Index";
               });
            services.AddCors();
            services.AddControllersWithViews();
            services.AddMvc().AddRazorRuntimeCompilation();
            services.AddDistributedMemoryCache();
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DBConnection"))
            );
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.Configure<ZohoMailOptions>(Configuration.GetSection("ZohoMail"));
            services.AddScoped<IEmailService, ZohoEmailService>();

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

            app.UseAuthorization();

            /* app.UseEndpoints(endpoints =>
             {
                 endpoints.MapControllerRoute(

                     name: "default",
                     pattern: "{controller=Home}/{action=Index}/{id?}");
             });*/

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Login}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                name: "default",
              pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
//test commit
