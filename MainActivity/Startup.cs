/*
 * Project: << InReachSolutions Assessment >>
 * Software Developer: Denis J Finkel
 * Start Date: June 10 , 2020
 * End Date: June 12 , 2020
 * Description: Create A Form That Uploads A File To AWS S3 & Then It Sends The File To Clients Email
 * Tools: Azure, AWS 
 * App Uses: Async-Threading
 * NOTE: Credentials Are Stored In Environment Variables 
 */
using MainActivity.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MainActivity {
    public class Startup {
        public Startup(IConfiguration configuration) => Configuration = configuration;
        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services) {
           
            // (CORE 3.00) Used by the Web Application (MVC) template.
            services.AddControllersWithViews();
            // (CORE 3.00) Equivalent To The Current AddMvc().
            services.AddControllers();

            // Initializes The Project Credentials 
            ProjectCredentials.Initialize(Configuration);
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            /****** Custom Routing Config ******/
            app.UseRouting();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "Default",
                    pattern: "{Controller=Home}/{Action=Index}/{id?}");
            });
        }
    }// Class ENDS 
}
