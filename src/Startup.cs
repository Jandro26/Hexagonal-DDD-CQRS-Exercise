using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hexagonal_Exercise.catalog.core.domain.commandBus;
using Hexagonal_Exercise.catalog.core.infrastructure;
using Hexagonal_Exercise.catalog.product.application.create;
using Hexagonal_Exercise.catalog.product.application.delete;
using Hexagonal_Exercise.catalog.product.application.find;
using Hexagonal_Exercise.catalog.product.application.update;
using Hexagonal_Exercise.catalog.product.domain;
using Hexagonal_Exercise.catalog.product.infrastructure;
using Hexagonal_Exercise.core.application;
using Hexagonal_Exercise.core.domain.eventBus;
using Hexagonal_Exercise.core.domain.queryBus;
using Hexagonal_Exercise.core.infrastructure;
using Hexagonal_Exercise.core.infrastructure.entityFramework;
using Hexagonal_Exercise.entry_point.catalog.v1.model;
using Hexagonal_Exercise.entry_point.catalog.v1.validator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace Hexagonal_Exercise
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddMvc().AddFluentValidation();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "hexagonal cqrs ddd api exercise",
                    Version = "v1",
                    Description = "This is an example of hexagonal architecture using DDD and CQRS.",
                    Contact = new OpenApiContact
                    {
                        Name = "Alex Reus",
                        Email ="reus.alex@gmail.com"
                    }

                });
            });

            services.AddTransient<IValidator<CreateProductModel>, CreateProductModelValidator>();

            services.AddTransient<ProductRepository, ProductSQLRepository>()
                    .AddSingleton<DomainEventBus, DomainEventBusDefault>()
                    .AddSingleton<CommandDispacher, CommandBusDefault>()
                    .AddSingleton<QueryDispacher, QueryBusDefault>();

            services.AddTransient<CommandHandler<CreateProductCommand>, CreateProductCommandHandler>()
                    .AddTransient<CommandHandler<RenameProductCommand>, RenameProductCommandHandler>()
                    .AddTransient<CommandHandler<DeleteProductCommand>, DeleteProductCommandHandler>()
                    .AddTransient<QueryHandler<FindProductQuery, FindProductQueryResult>, FindProductQueryHandler>();
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
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog API V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
