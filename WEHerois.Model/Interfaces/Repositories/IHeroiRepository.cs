using WEHerois.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WEHerois.Model.Interfaces.Repositories
{
    public interface IHeroiRepository
    {
        Task<IEnumerable<HeroiEntity>> GetAllAsync();
        Task<HeroiEntity> GetByIdAsync(int id);
        Task InsertAsync(HeroiEntity heroiEntity);
        Task UpdateAsync(HeroiEntity heroiEntity);
        Task DeleteAsync(HeroiEntity heroiEntity);
    }
}
