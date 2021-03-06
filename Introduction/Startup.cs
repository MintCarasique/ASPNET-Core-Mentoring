using System;
using System.IO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Northwind.Core.Models;
using Northwind.Core.Repositories;
using Northwind.Core.Services;
using Northwind.Filters;
using Northwind.Middleware;
using Northwind.Models;
using Northwind.Services;

namespace Northwind
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
            services.AddMvc().AddRazorOptions(options => options.AllowRecompilingViewsOnFileChange = true);
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IRepository<Category>, CategoryRepository>();
            services.AddScoped<IRepository<Product>, ProductRepository>();
            services.AddScoped<IRepository<Supplier>, SupplierRepository>();

            services.AddSingleton<IConfigurationService, ConfigurationService>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);

            Action<MvcOptions> configMvcAction = x => { };

            services.AddAuthentication(sharedOptions =>
            {

            }).AddAzureAD(options => Configuration.Bind("AzureAd", options)).AddCookie();
            var isLoggingEnabled = Configuration.GetSection("Logging").GetValue<bool>("ActionLoggingEnabled");
            if (isLoggingEnabled)
                configMvcAction = options => options.Filters.Add(typeof(LoggingFilterAttribute));

            services.AddMvc(configMvcAction);
            services.AddSwaggerDocument();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env,
            IApplicationLifetime applicationLifetime)
        {
            //applicationLifetime.ApplicationStarted.Register(OnApplicationStarted);
            applicationLifetime.ApplicationStopping.Register(OnApplicationStopping);
            applicationLifetime.ApplicationStopped.Register(OnApplicationStopped);

            _logger.LogInformation("Application started successfully");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler("/Error/exceptionError");
            }
            app.UseImageCaching();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            //app.UseStatusCodePagesWithRedirects("/Error/HttpError");
            app.UseOpenApi();
            app.UseSwaggerUi3();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(name: "images",
                    template: "images/{id?}",
                    defaults: new
                    {
                        controller = "Categories",
                        action = "Image"
                    });
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
