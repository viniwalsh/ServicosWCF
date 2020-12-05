using WEHerois.Model.Entities;
using WEHerois.Model.Interfaces.Infrastructure;
using WEHerois.Model.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WEHerois.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroiController : ControllerBase
    {
        private readonly IHeroiService _service;
        private readonly IBlobService _blobService;
        private readonly IConfiguration _configuration;

        public HeroiController(IConfiguration configuration, IHeroiService service, IBlobService blobService)
        {
            _configuration = configuration;
            _service = service;
            _blobService = blobService;
        }

        // GET: api/Heroi
        [HttpGet]
        [ProducesResponseType(typeof(HeroiEntity), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<HeroiEntity>>> GetAll()
        {
            var herois = (await _service.GetAllAsync()).ToList();
            return herois;
        }

        // GET: api/Heroi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HeroiEntity>> Get(int id)
        {
            var heroi = await _service.GetByIdAsync(id);

            if (heroi == null) return NotFound();

            return heroi;
        }

        // PUT: api/Heroi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromForm] HeroiEntity heroi, [FromForm] IFormFile file)
        {
            if (await _service.GetByIdAsync(heroi.Id) == null)
            {
                return NotFound();
            }

            await _service.UpdateAsync(heroi, file?.OpenReadStream());
            
            return NoContent();
        }

        // POST: api/Heroi
        [HttpPost]
        public async Task<ActionResult<HeroiEntity>> Post([FromForm] HeroiEntity heroi, [FromForm] IFormFile file)
        {
            await _service.InsertAsync(heroi, file?.OpenReadStream());

            return CreatedAtAction(nameof(Get), new { id = heroi.Id }, heroi);
        }

        // DELETE: api/Heroi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HeroiEntity>> Delete(int id)
        {
            var heroi = await _service.GetByIdAsync(id);
            if (heroi == null)
            {
                return NotFound();
            }

            await _service.DeleteAsync(heroi);
            return heroi;
        }
    }
}