using WEHerois.Model.Entities;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace WEHerois.Model.Interfaces.Services
{
    public interface IHeroiService
    {
        Task<IEnumerable<HeroiEntity>> GetAllAsync();
        Task<HeroiEntity> GetByIdAsync(int id);
        Task InsertAsync(HeroiEntity heroiEntity, Stream stream);
        Task UpdateAsync(HeroiEntity heroiEntity, Stream stream);
        Task DeleteAsync(HeroiEntity heroiEntity);
    }
}
