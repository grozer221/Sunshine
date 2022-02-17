using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sunshine.Repositories;
using Sunshine.Services;

namespace Sunshine
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
            services.AddDbContext<AppDatabaseContext>(options => options.UseMySQL("server=localhost;user=root;password=;database=sunshine;"));

            services.AddScoped<UsersRepository>();
            services.AddScoped<MenusRepository>();
            services.AddScoped<SubMenusRepository>();
            services.AddScoped<NewsRepository>();
            services.AddScoped<FilesRepository>();

            services.AddScoped<AuthRepository>();

            services.AddSingleton<MailService>();
            services.AddSingleton<AuthService>();

            services.AddControllersWithViews();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Auth/Login");
                    options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Auth/AccessDenied");
                });
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

            app.UseAuthentication();    // аутентификация
            app.UseAuthorization();     // авторизация  

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                 name: "areas",
                 pattern: "{area:exists}/{controller=News}/{action=Index}/{id?}"
                 );

                endpoints.MapControllerRoute(
                 name: "default",
                 pattern: "{controller=News}/{action=Index}/{id?}"
                 );
            });
        }
    }
}
