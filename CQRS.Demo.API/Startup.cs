using CQRS.Demo.Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace CQRS.Demo.API
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
            var assemblies = new[]
            {
                Assembly.GetAssembly(typeof(AssemblyIdentifier)),
                Assembly.GetAssembly(typeof(Domain.AssemblyIdentifier)),
                Assembly.GetAssembly(typeof(DomainServices.AssemblyIdentifier))
            };

            IocContainerConfigurationExtensions.RegisterTypesContainerControlledByConvention(services.TryAddScoped, assemblies);
            IocContainerConfigurationExtensions.RegisterTypesSingletonByConvention(services.TryAddSingleton, assemblies);
            IocContainerConfigurationExtensions.RegisterTypesTransientByConvention(services.TryAddTransient, assemblies);

            string connectionString = Configuration.GetValue<string>("ConnectionString");
            services.RemoveAll<IDomainContextFactory>();
            services.RemoveAll<DomainContextFactory>();
            var domainContextFactory = new DomainContextFactory(connectionString);
            services.AddScoped<IDomainContextFactory>(x => x.GetRequiredService<DomainContextFactory>());
            services.AddScoped(x => domainContextFactory);
            services.AddScoped<IDomainReadonlyContext>(x => x.GetRequiredService<DomainReadonlyContext>());
            services.AddScoped(x => new DomainReadonlyContext(domainContextFactory));

            services.AddControllers();

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });
            services.AddVersionedApiExplorer(options =>
            {
                options.SubstituteApiVersionInUrl = true;
                options.GroupNameFormat = "'v'VVV";
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CQRS Demo API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CQRS Demo API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
