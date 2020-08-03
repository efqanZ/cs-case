using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using CiSeCase.Api.CrossCutting;
using CiSeCase.Core.Interfaces.Manager;
using CiSeCase.Core.Interfaces.Repository;
using CiSeCase.Core.Models;
using CiSeCase.Core.Services.BasketUseCases;
using CiSeCase.Infrastructure.Data;
using CiSeCase.Infrastructure.Data.Repository;
using CiSeCase.Infrastructure.Managers.Cache;
using CiSeCase.Infrastructure.Managers.Hash;
using CiSeCase.Infrastructure.Managers.Map;
using CiSeCase.Infrastructure.Validation;
using CiSeCase.Infrastructure.Validation.Basket;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace CiSeCase.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddConfiguration(configuration)
               .AddJsonFile("appsettings.json", true, true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
               .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //ToDo: Test yaz.


            RegisterDatas(services);
            RegisterManagers(services);
            RegisterCors(services);
            RegisterSwagger(services);

            services.AddAutoMapper(typeof(AutoMapperMapManager));

            services.AddMediatR(typeof(AddProductToBasketHandler).GetTypeInfo().Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddValidatorsFromAssembly(typeof(AddProductToBasketRequestValidator).Assembly);

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ExceptionFilterAttribute));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseRequestResponseLoggingMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Çiçek Sepeti Case Api Documentation");
            });


            app.UseCors("AllowAll");

            SeedData.Initialize(app.ApplicationServices);
        }

        private void RegisterDatas(IServiceCollection services)
        {
            services.AddEntityFrameworkNpgsql().AddDbContext<AppDbContext>(opt =>
                opt.UseNpgsql(Configuration.GetConnectionString("PostgreConnection"))
            );

            services.AddScoped<DbContext, AppDbContext>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
        }

        private void RegisterManagers(IServiceCollection services)
        {
            services.AddSingleton<IHashManager, Sha256HashManager>();

            services.AddSingleton<RedisServer>();
            services.AddSingleton<ICacheManager, RedisCacheManager>();


            services.AddSingleton<IMapManager, AutoMapperMapManager>();
        }

        private void RegisterCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                   builder =>
                   {
                       builder.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
                   });
            });
        }

        private void RegisterSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Çiçek Sepeti Case Api", Version = "v1" });
            });
        }

    }
}
