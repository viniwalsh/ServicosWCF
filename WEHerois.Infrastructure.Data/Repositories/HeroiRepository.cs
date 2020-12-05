using WEHerois.Model.Entities;
using WEHerois.Model.Interfaces.Repositories;
using WEHerois.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WEHerois.Infrastructure.Data.Repositories
{
    public class HeroiRepository : IHeroiRepository
    {
        private readonly WEHeroiContext _context;

        public HeroiRepository(WEHeroiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HeroiEntity>> GetAllAsync()
        {
            return await _context.HeroiEntity.ToListAsync();
        }

        public async Task<HeroiEntity> GetByIdAsync(int id)
        {
            return await _context.HeroiEntity.AsNoTracking().SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task InsertAsync(HeroiEntity heroiEntity)
        {
            _context.Add(heroiEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(HeroiEntity heroiEntity)
        {
            _context.Update(heroiEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(HeroiEntity heroiEntity)
        {
            _context.Remove(heroiEntity);
            await _context.SaveChangesAsync();
        }
    }
}
