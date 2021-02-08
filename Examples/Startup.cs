using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Westwind.AspNetCore.LiveReload;

namespace Examples
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            StaticConfiguration = configuration;
        }

        public IConfiguration Configuration { get; }
        public static IConfiguration StaticConfiguration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();

            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddCarter(options =>
            {
                options.OpenApi.Enabled = false;
            });

            services.AddLiveReload(config =>
            {
                config.LiveReloadEnabled = true;
            });

            //doet dit normaal gesproken niet!
            services.AddAntiforgery(options =>
            {
                options.SuppressXFrameOptionsHeader = true;
            });
            //services.AddDirectoryBrowser();
            //services.AddAntiforgery(options => options.HeaderName = "XSRF-TOKEN");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // //middleware A
            // app.Use(async (context, next) =>
            // {
            //     if (context.Request.Path.Value
            //         .StartsWith("/lesson0/getRequestPipelineExample",
            //             StringComparison.OrdinalIgnoreCase))
            //     {
            //         //Write to the context.Response.Body
            //         await context.Response.WriteAsync("<h1>Hello World</h1>");
            //         context.Response.Headers.Add("Content-Type", "text/html");
            //         context.Response.StatusCode = (int)HttpStatusCode.OK; //HttpStatusCode.OK == 200
            //     }
            //     else
            //     {
            //         await next.Invoke();
            //     }
            // });
            //
            // //middleware B
            // // app.Use(async (context, next) =>
            // // {
            // //     ...
            // //     await next.Invoke();
            // // });
            //
            // app.Run(async context =>
            // {
            //     await context.Response.WriteAsync("<h1>Page Not Found</h1>");
            // });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseLiveReload();
                app.UseBrowserLink();
            }
            else
            {
                //app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseStaticFiles();
            //app.UseFileServer(enableDirectoryBrowsing: true);

            app.UseSession();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapCarter();
            });
        }
    }
}
