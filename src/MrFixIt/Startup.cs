using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MrFixIt.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MrFixIt
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {   // configureservices method is were we configure framework services to our application. 
            // adds the MVC service to the project.
            services.AddMvc();

            services.AddEntityFramework()
                .AddDbContext<MrFixItContext>(options =>
                    options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<MrFixItContext>()
                .AddDefaultTokenProviders();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // configure method is were we tell ASP.NET what frameworks we want to use in our app.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {   
            // Order matters and the static file needs to run before app.run.
           // static file here allows us to use our static files example anything in our root folder like css, Js , img etc.
            app.UseStaticFiles();

            loggerFactory.AddConsole();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // allows access for Identity = authentication
            app.UseIdentity();
            //tells our app that we will be using MVC framework
            app.UseMvc(routes =>
            {
                //routes are default page rather than home its account in this case, any parameters will be passed as an id.
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Index}/{id?}");
            });
            app.Run(async (error) =>
            {
                await error.Response.WriteAsync("You should not see this message. An error has occured.");
            });
        }
    }
}
