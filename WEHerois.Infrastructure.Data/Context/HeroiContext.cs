using WEHerois.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace WEHerois.Infrastructure.Data.Context
{
    public class WEHeroiContext : DbContext
    {
        public WEHeroiContext (DbContextOptions<WEHeroiContext> options) : base(options) { }

        public DbSet<HeroiEntity> HeroiEntity { get; set; }
    }
}