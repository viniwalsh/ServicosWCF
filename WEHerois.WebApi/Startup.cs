using WEHerois.Infrastructure.Services.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WEHerois.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using WEHerois.Model.Interfaces.Repositories;
using WEHerois.Model.Interfaces.Services;
using WEHerois.Services.Services;
using WEHerois.Infrastructure.Data.Repositories;
using WEHerois.Model.Interfaces.Infrastructure;
using WEHerois.Infrastructure.Services.Queue;
using WEHerois.Infrastructure.Services.Blob;

namespace WEHerois.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            services.AddDbContext<WEHeroiContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("WEHeroisContext")));

            services.AddScoped<IHeroiRepository, HeroiRepository>();
            services.AddScoped<IHeroiService, HeroiService>();

            services.AddScoped<IBlobService, BlobService>(provider =>
            new BlobService(Configuration.GetValue<string>("StorageAccount")));

            services.AddScoped<IQueueService, QueueService>(provider =>
                new QueueService(Configuration.GetValue<string>("StorageAccount")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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