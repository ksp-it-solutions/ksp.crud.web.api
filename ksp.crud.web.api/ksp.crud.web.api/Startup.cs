using ksp.crud.web.api.DataModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ksp.crud.web.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options => 
                options.UseSqlServer(Configuration["Data:ConnectionStrings:DefaultConnection"])
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.WithOrigins("http://localhost:8080/").AllowAnyHeader().AllowAnyMethod());
            });
            services.AddControllers(config =>
            {
                config.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
            }).AddXmlDataContractSerializerFormatters();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ksp.crud.web.api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ksp.crud.web.api v1"));
            }

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter()
        {
            var build = new ServiceCollection()
                .AddLogging()
                .AddMvc()
                .AddNewtonsoftJson()
                .Services.BuildServiceProvider();

            return build.GetRequiredService<IOptions<MvcOptions>>().Value.InputFormatters.OfType<NewtonsoftJsonPatchInputFormatter>().First();
        }
    }
}
