
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShoppingApi.Controllers;
using ShoppingApi.Domain;
using ShoppingApi.Profiles;
using ShoppingApi.Services;
using System.Text.Json.Serialization;

namespace ShoppingApi
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
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                });
            services.AddDbContext<ShoppingDataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("shopping"))
            );

            //var mapperConfiguration = Configuration.GetSection<ConfigurationForMapper>("Mapper");
            // services.Configure<ConfigurationForMapper>(Configuration.GetSection("Mapper"));

            // this is what we are going to use for Automapper
            var configForMapper = new ConfigurationForMapper();
            Configuration.GetSection(configForMapper.SectionName).Bind(configForMapper);

            // this sets up an IOptions<ConfigurationForMapper> that we can inject into other dependencies
            services.Configure<ConfigurationForMapper>(Configuration.GetSection(configForMapper.SectionName));

            var mapperConfig = new MapperConfiguration(opt =>
           {
               opt.AddProfile(new CatalogProfile(configForMapper));
           });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton<IMapper>(mapper);
            services.AddSingleton<MapperConfiguration>(mapperConfig);
            services.AddTransient<IDoCurbsideQueries, EntityFrameworkCurbsideData>();
            services.AddScoped<IDoCurbsideCommands, EntityFrameworkCurbsideData>();
            services.AddSingleton<CurbsideChannel>();
            services.AddHostedService<CurbsideOrderProcessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
