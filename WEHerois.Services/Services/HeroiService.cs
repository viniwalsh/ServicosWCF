using WEHerois.Model.Entities;
using WEHerois.Model.Interfaces.Infrastructure;
using WEHerois.Model.Interfaces.Repositories;
using WEHerois.Model.Interfaces.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace WEHerois.Services.Services
{
    public class HeroiService : IHeroiService
    {
        private readonly IHeroiRepository _repository;
        private readonly IBlobService _blobService;
        private readonly IQueueService _queueService;

        public HeroiService(IHeroiRepository repository, 
                            IBlobService blobService,
                            IQueueService queueService)
        {
            _repository = repository;
            _blobService = blobService;
            _queueService = queueService;
        }

        public async Task<IEnumerable<HeroiEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<HeroiEntity> GetByIdAsync(int id)
        {
            var heroi = await _repository.GetByIdAsync(id);

            var jsonHeroi = JsonConvert.SerializeObject(heroi);
            var bytesJsonHeroi = UTF8Encoding.UTF8.GetBytes(jsonHeroi);
            string jsonHeroiBase64 = Convert.ToBase64String(bytesJsonHeroi);

            await _queueService.SendAsync(jsonHeroiBase64);

            return heroi;
        }

        public async Task InsertAsync(HeroiEntity heroiEntity, Stream stream)
        {
            var newUri = await _blobService.UploadAsync(stream);
            heroiEntity.ImageUri = newUri;

            await _repository.InsertAsync(heroiEntity);
        }

        public async Task UpdateAsync(HeroiEntity heroiEntity, Stream stream)
        {
            if (stream != null)
            {
                await _blobService.DeleteAsync(heroiEntity.ImageUri);

                var newUri = await _blobService.UploadAsync(stream);
                heroiEntity.ImageUri = newUri;
            }

            await _repository.UpdateAsync(heroiEntity);
        }

        public async Task DeleteAsync(HeroiEntity heroiEntity)
        {
            await _blobService.DeleteAsync(heroiEntity.ImageUri);

            await _repository.DeleteAsync(heroiEntity);
        }
    }
}