using System;
using System.Collections.Generic;
using Dapper;
using FactoryMethod.Entities;
using FactoryMethod.Enums;
using FactoryMethod.Extensions;
using FactoryMethod.Factory;
using FactoryMethod.Interfaces;
using FactoryMethod.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace FactoryMethod
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
            var databaseStorageSection = Configuration.GetSection("ConfigAPI");
            services.Configure<ConfigAPI>(databaseStorageSection);

            var databaseStorage = databaseStorageSection.GetSection("DatabaseStorage");

            if (databaseStorage.Value == "SqLite")
            {
                SqlMapper.AddTypeHandler(new MySqlGuidTypeHandler());
                SqlMapper.RemoveTypeMap(typeof(Guid));
                SqlMapper.RemoveTypeMap(typeof(Guid?));
            }

            var connectionDict = new Dictionary<DatabaseConnectionName, string>
            {    
                
                { DatabaseConnectionName.SQLServer, this.Configuration.GetConnectionString("SqlServerContext") },
                { DatabaseConnectionName.SqLite, this.Configuration.GetConnectionString("SqLiteContext") }
            };             

            // Inject this dict
            services.AddSingleton<IDictionary<DatabaseConnectionName, string>>(connectionDict);

            // Inject the factory
            services.AddTransient<IDbConnectionFactory, DapperDbConnectionFactory>();
            services.AddScoped<IAlunoRepositorySQLServer, AlunoRepositorySQLServer>();
            services.AddScoped<IAlunoRepositorySqLite, AlunoRepositorySqLite>();
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FactoryMethod", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FactoryMethod v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
