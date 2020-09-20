using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WarehouseGrpc.Data;
using WarehouseGrpc.Helpers;
using WarehouseGrpc.Model;
using WarehouseGrpc.Services;
using WarehouseGrpc.Validators;

namespace WarehouseGrpc
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;

        }

        public IConfiguration Configuration { get; set; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();

            services.AddDbContext<DataContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddAutoMapper(x => x.AddProfile(new AutoMapperProfiles()));
            services.AddScoped<IEquipmentRepository, EquipmentRepository>();
            services.AddMvc(setup => {}).AddFluentValidation();
            services.AddTransient<IValidator<Equipment>, EquipmentValidator>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapGrpcService<EquipmentService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
