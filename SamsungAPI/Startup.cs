using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SamsungAPI.Data;

namespace SamsungAPI
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
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<DataContext>(config =>
            {
                config.UseSqlServer(connectionString);
            });
            services.AddControllersWithViews();

            services.AddAuthentication(o =>
            {
                o.DefaultScheme = "Cookie";
                o.DefaultChallengeScheme = "oidc";
            })
                .AddJwtBearer("Bearer", config =>
                {
                    config.Authority = "https://localhost:44312/";
                    config.Audience = "SamsungAPI";
                })
                .AddCookie("Cookie")
                .AddOpenIdConnect("oidc", config => {
                    config.Authority = "https://localhost:44312/";
                    config.ClientId = "samsung_id";
                    config.ClientSecret = "samsung_secret";
                    config.SaveTokens = true;
                    config.ResponseType = "code";
                    config.SignedOutCallbackPath = "/home/index";


                    //smaller id token but two trips
                    config.GetClaimsFromUserInfoEndpoint = true;

                    //claim maping
                    config.ClaimActions.MapUniqueJsonKey("samsung.admin", "samsung.admin");
                    //config.ClaimActions.MapUniqueJsonKey("rc.api.garndma", "rc.api.garndma");

                    //configure scope
                    //config.Scope.Clear();
                    config.Scope.Add("openid");
                    config.Scope.Add("samsung.user");
                  
                }); 
        }
  
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
            SeedData.SeedDatabase(services.GetRequiredService<DataContext>());
        }
    }
}
