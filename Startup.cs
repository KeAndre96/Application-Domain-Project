using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using AppDomainProject.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using AppDomainProject.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace AppDomainProject
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
            services.AddRazorPages();

            services.AddDbContext<AppDomainProjectContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("AppDomainProjectContext")));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {
                
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("registered", policy => policy.Requirements.Add(new UserAuthorizationRequirement()));
                options.AddPolicy("user", policy => policy.Requirements.Add(new UserAuthorizationRequirement(AccountType.User)));
                options.AddPolicy("manager", policy => policy.Requirements.Add(new UserAuthorizationRequirement(AccountType.Manager)));
                options.AddPolicy("admin", policy => policy.Requirements.Add(new UserAuthorizationRequirement(AccountType.Admin)));
            });

            services.AddSingleton<IAuthorizationHandler, UserAuthorizationHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDomainProjectContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Lax });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

            new DBInitializer(context).Init();
        }
    }
}
