using API_EndPoint_220522.Caching;
using API_EndPoint_220522.Contexts;
using API_EndPoint_220522.Mapper;
using API_EndPoint_220522.Models.SignalR;
using API_EndPoint_220522.Repositories;
using API_EndPoint_220522.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace API_EndPoint_220522
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddLocalization(opt => opt.ResourcesPath = "Resources");
            services.AddDbContext<EmployeeDBContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("MyConnection")));
            services.AddStackExchangeRedisCache(opt => opt.Configuration = "localhost:6379");
            services.AddScoped<IEmployeeDBContext, EmployeeDBContext>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ICacheManager, CacheManager>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            services.AddAutoMapper(typeof(Mapping));
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            var supportedCultures = new[] { "en-US", "fr-FR","ta-IN" };
            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<BroadCastHub>("/notify");
                endpoints.MapHub<MyHub>("/message");
            });
        }
    }
}
