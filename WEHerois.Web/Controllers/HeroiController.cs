using WEHerois.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace WEHerois.Web.Controllers
{
    public class HeroiController : Controller
    {
        private readonly HttpClient _client;
        private const string RESOURCE = @"/api/heroi";

        public HeroiController(IHttpClientFactory service)
        {
            _client = service.CreateClient("backend");
        }

        // GET: Amigo
        public async Task<IActionResult> Index()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, RESOURCE);

            var httpResponse = await _client.SendAsync(request);

            httpResponse.EnsureSuccessStatusCode();

            var responseJsonString = await httpResponse.Content.ReadAsStringAsync();

            var herois = JsonConvert.DeserializeObject<IEnumerable<HeroiEntity>>(responseJsonString);

            return View(herois);
        }

        // GET: Heroi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var heroi = await GetById(id.Value);

            return View(heroi);
        }

        // GET: Heroi/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Amigo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HeroiEntity heroiEntity, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                var stream = ImageFile.OpenReadStream();
                byte[] data;
                using (var binaryReader = new BinaryReader(stream))
                {
                    data = binaryReader.ReadBytes((int)stream.Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                var multiPart = new MultipartFormDataContent();
                multiPart.Add(bytes, "file", "imagem.jpg");

                multiPart.Add(new StringContent(heroiEntity.Id.ToString()), "Id");
                multiPart.Add(new StringContent(heroiEntity.Nome), "Nome");
                multiPart.Add(new StringContent(heroiEntity.Codinome), "Codinome");
                multiPart.Add(new StringContent(heroiEntity.Lancamento.ToString()), "Lancamento");
                multiPart.Add(new StringContent(heroiEntity.Poder.ToString()), "Parente");
                multiPart.Add(new StringContent(heroiEntity.ImageUri == null ? "" : heroiEntity.ImageUri), "ImageUri");

                var response = await _client.PostAsync(RESOURCE, multiPart);

                response.EnsureSuccessStatusCode();

                return RedirectToAction(nameof(Index));
            }
            return View(heroiEntity);
        }

        // GET: Amigo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var heroi = await GetById(id.Value);

            return View(heroi);
        }

        // POST: Amigo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, HeroiEntity heroiEntity, IFormFile ImageFile)
        {
            if (id != heroiEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var stream = ImageFile.OpenReadStream();
                byte[] data;
                using (var binaryReader = new BinaryReader(stream))
                {
                    data = binaryReader.ReadBytes((int)stream.Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                var multiPart = new MultipartFormDataContent();
                multiPart.Add(bytes, "file", "imagem.jpg");

                multiPart.Add(new StringContent(heroiEntity.Id.ToString()), "Id");
                multiPart.Add(new StringContent(heroiEntity.Nome), "Nome");
                multiPart.Add(new StringContent(heroiEntity.Codinome), "Codinome");
                multiPart.Add(new StringContent(heroiEntity.Lancamento.ToString()), "Lancamento");
                multiPart.Add(new StringContent(heroiEntity.Poder.ToString()), "Parente");
                multiPart.Add(new StringContent(heroiEntity.ImageUri == null ? "" : heroiEntity.ImageUri), "ImageUri");

                var response = await _client.PutAsync($"{RESOURCE}/{id}", multiPart);

                response.EnsureSuccessStatusCode();

                return RedirectToAction(nameof(Index));
            }
            return View(heroiEntity);
        }

        // GET: Heroi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var heroi = await GetById(id.Value);

            return View(heroi);
        }

        // POST: Heroi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{RESOURCE}/{id}");

            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return RedirectToAction(nameof(Index));
        }

        private async Task<HeroiEntity> GetById(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{RESOURCE}/{id}");

            var httpResponse = await _client.SendAsync(request);

            httpResponse.EnsureSuccessStatusCode();

            var responseJsonString = await httpResponse.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<HeroiEntity>(responseJsonString);
        }
    }
}
