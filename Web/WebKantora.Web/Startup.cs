using AspNetSeo.CoreMvc;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PaulMiami.AspNetCore.Mvc.Recaptcha;
using System;
using WebKantora.Data;
using WebKantora.Data.Common;
using WebKantora.Data.Common.Contracts;
using WebKantora.Data.Models;
using WebKantora.Services.Web;
using WebKantora.Services.Web.Contracts;
using WebKantora.Web.Infrastructure.Extensions;
using WebKantora.Web.Services;

namespace WebKantora.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<WebKantoraDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<WebKantoraDbContext, Guid>()
                .AddDefaultTokenProviders();
            
            services.AddTransient(typeof(IWebKantoraDbRepository<>), typeof(WebKantoraDbRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddDomainServices();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddMemoryCache();
            services.AddAutoMapper();
            services.AddMvc();

            services.AddSeoHelper();
            services.AddSession();
            services.AddRecaptcha(new RecaptchaOptions
                {
                    SiteKey = Configuration["Recaptcha:SiteKey"],
                    SecretKey = Configuration["Recaptcha:SecretKey"]
                });
            // Add application services.
            services.AddTransient<IEmailSenderService, EmailSenderService>();
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            WebKantoraDbContext context,
            RoleManager<Role> roleManager)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();


            app.UseIdentity();
            RolesData.SeedRoles(roleManager).Wait();


            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseStatusCodePagesWithRedirects("/Home/Error");
                //app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseStatusCodePagesWithRedirects("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            app.UseSession();
            //AutoMapperConfig automapperConfig = new AutoMapperConfig();
            //automapperConfig.Execute(Assembly.GetEntryAssembly());
            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                  routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Blog}/{action=Index}/{id?}"
                );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            DbInitializer.Initialize(context, app);
        }
    }
}
