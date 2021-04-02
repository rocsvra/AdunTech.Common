using AdunTech.ExceptionDetail;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdunTech.HttpException.Demo
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddProblemDetails(ConfigureProblemDetails);
            services.AddControllers()
                .AddJsonOptions(x => x.JsonSerializerOptions.IgnoreNullValues = true);


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AdunTech.HttpException.Demo", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AdunTech.HttpException.Demo v1"));
            }
            app.UseProblemDetails();

            app.Use(CustomMiddleware);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private Task CustomMiddleware(HttpContext context, Func<Task> next)
        {
            if (context.Request.Path.StartsWithSegments("/middleware", out _, out var remaining))
            {
                if (remaining.StartsWithSegments("/error"))
                {
                    throw new Exception("This is an exception thrown from middleware.");
                }

                if (remaining.StartsWithSegments("/status", out _, out remaining))
                {
                    var statusCodeString = remaining.Value.Trim('/');

                    if (int.TryParse(statusCodeString, out var statusCode))
                    {
                        context.Response.StatusCode = statusCode;
                        return Task.CompletedTask;
                    }
                }
            }

            return next();
        }

        private void ConfigureProblemDetails(ProblemDetailsOptions options)
        {
            options.IncludeExceptionDetails = (ctx, ex) => Environment.IsDevelopment();

            options.Rethrow<NotSupportedException>();
            options.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);
            options.MapToStatusCode<HttpRequestException>(StatusCodes.Status503ServiceUnavailable);
            //=====自定义异常=====
            string errUrl = "http://aduntech.com/error/codes/";
            options.Map<ResourceNotFoundException>(ex => new ResourceNotFoundExceptionDetails(errUrl, ex));
            options.Map<BadRequestException>(ex => new BadRequestExceptionDetails(errUrl, ex));
            options.Map<InternalServerException>(ex => new InternalServerExceptionDetails(errUrl, ex));
            //=====自定义异常=====
            options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
        }
    }
}
