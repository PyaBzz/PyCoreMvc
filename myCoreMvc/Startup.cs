﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using PooyasFramework.Middleware;
using PooyasFramework;
using myCoreMvc.Services;
using myCoreMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;

namespace myCoreMvc
{
    public class Startup
    {
        // This method gets called by the runtime.
        // Use it to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => { options.Filters.Add(new RequireHttpsAttribute()); });

            #region I use my own service container because:
            // Injecting registered services is only possible by:
            // 1- Constructors of controllers and middleware
            // 2- Invoke method of middleware
            // 3- HttpContext.RequestServices
            //Making registered services available in other classes (e.g service classes) is so complex and difficult and requires bad programming practices.
            #endregion
            ServiceInjector.Register<IDataProvider, DbMock>(Injection.Singleton);

            var users = new Dictionary<string, string> { { "Hasang", "Palang" } };
            services.AddSingleton<IUserService>(new UserServiceMock(users));
        }

        //TODO: Experiment with cookies.
        //TODO: Implement user authentication with cookies: https://docs.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-2.1&tabs=aspnetcore2x
        //TODO: Use OnActionExecuting() and OnActionExecuted() methods from here: https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controller.onactionexecuting?view=aspnetcore-2.1

        // This method gets called by the runtime.
        // Use it to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            app.UseMiddleware<CustomMiddleware>();

            app.UseMiddleware<AntiForgeryTokenValidatorMiddleware>();

            app.UseRewriter(new RewriteOptions().AddRedirectToHttps(301, 44383));

            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            var staticFileOptions = new StaticFileOptions();
            staticFileOptions.RequestPath = "/StaticContent";
            var path = Path.Combine(env.ContentRootPath, "StaticFiles");
            staticFileOptions.FileProvider = new PhysicalFileProvider(path);
            app.UseStaticFiles(staticFileOptions);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=ListOfWorkItems}/{action=Index}/{id?}");
            });
        }
    }
}
