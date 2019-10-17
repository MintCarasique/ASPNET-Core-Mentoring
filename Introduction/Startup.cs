using Introduction.Models;
using Introduction.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Introduction
{
    public class Startup
    {
        private readonly ILogger _logger;

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("NorthwndConf");
            services.AddDbContext<NorthwindContext>(options => options.UseSqlServer(connectionString));
            services.AddMvc();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISupplierService, SupplierService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env,
            IApplicationLifetime applicationLifetime)
        {
            applicationLifetime.ApplicationStarted.Register(OnApplicationStarted);
            applicationLifetime.ApplicationStopping.Register(OnApplicationStopping);
            applicationLifetime.ApplicationStopped.Register(OnApplicationStopped);

            _logger.LogInformation("Application started successfully");
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/Error/exceptionError");
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseStatusCodePagesWithRedirects("/Error/HttpError");
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void OnApplicationStarted()
        {
            var children = Configuration.GetChildren();
            foreach (var section in children)
            {
                _logger.LogInformation($"Configuration section '{section.Key}' with value '{section.Value}' successfully loaded.");
            }
            _logger.LogInformation("Application started.");
        }

        private void OnApplicationStopping()
        {
            _logger.LogInformation("Stopping application...");
        }

        private void OnApplicationStopped()
        {
            _logger.LogInformation("Application stopped successfully");
        }
    }
}
