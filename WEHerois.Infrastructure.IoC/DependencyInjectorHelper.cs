using WEHerois.Model.Interfaces.Infrastructure;
using WEHerois.Model.Interfaces.Repositories;
using WEHerois.Model.Interfaces.Services;
using WEHerois.Services.Services;
using WEHerois.Infrastructure.Data.Context;
using WEHerois.Infrastructure.Data.Repositories;
using WEHerois.Infrastructure.Services.Blob;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WEHerois.Infrastructure.Services.Queue;

namespace WEHerois.Infrastructure.Services.IoC
{
    public class DependencyInjectorHelper
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WEHeroiContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("WEHeroisContext")));

            services.AddScoped<IHeroiRepository, HeroiRepository>();
            services.AddScoped<IHeroiService, HeroiService>();

            services.AddScoped<IBlobService, BlobService>(provider => 
            new BlobService(configuration.GetValue<string>("StorageAccount")));

            services.AddScoped<IQueueService, QueueService>(provider =>
                new QueueService(configuration.GetValue<string>("StorageAccount")));
        }
    }
}
