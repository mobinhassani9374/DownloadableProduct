using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DownloadableProduct.Identity;
using DownloadableProduct.Identity.DataModel;
using Microsoft.AspNetCore.Identity;
using DownloadableProduct.UI.Helpers;
using DownloadableProduct.DataAccess.Repositories;
using DownloadableProduct.Services;

namespace DownloadableProduct.UI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(@"Data Source=185.51.200.186\SQL2014,2014;Initial Catalog=DownloadableProduct;Persist Security Info=True;User ID=downloadable_mobin; Password=Whh&872g; MultipleActiveResultSets=True");
            });

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
             .AddEntityFrameworkStores<AppDbContext>()
             .AddClaimsPrincipalFactory<AppUserClaimsPrincipalFactory>()
             .AddDefaultTokenProviders();


            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/login";
                options.LogoutPath = "/logout";
                options.AccessDeniedPath = "/accessDenied";
                options.SlidingExpiration = true;
                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true,
                    Name = ".Fiver.Security.Cookie",
                    Path = "/",
                    SameSite = SameSiteMode.Lax,
                    SecurePolicy = CookieSecurePolicy.SameAsRequest
                };
            });

            services.AddScoped<UserManagerService>();
            services.AddScoped<SignInManagerService>();

            services.AddScoped<FileHelper>();

            services.AddScoped<ProductRepository>();
            services.AddScoped<UserRepository>();
            services.AddScoped<ProductService>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, AppDbContext db, RoleManager<IdentityRole> roleManager)
        {
            db.Database.Migrate();

            new SeedData().Initial(roleManager);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
