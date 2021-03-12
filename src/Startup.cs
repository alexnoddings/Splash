using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Splash.Data;

namespace Splash
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            services.AddSingleton<IConfiguration>(_configuration);
            // Adding as a transient allows configuration to be modified without restarting
            services.AddTransient<Info>(innerServices => innerServices.GetRequiredService<IConfiguration>().GetSection("Info").Get<Info>());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
                app.UseHsts();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapFallback("{**catch-all}", httpContext =>
                {
                    httpContext.Response.Redirect("/", false);
                    return Task.CompletedTask;
                });
            });
        }
    }
}
