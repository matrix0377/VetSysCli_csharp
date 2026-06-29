using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VetSysCli.Application.Interfaces;
using VetSysCli.Application.DTOs;

namespace VetSysCli.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalService _service;
        public AnimalsController(IAnimalService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string query)
        {
            var list = await _service.SearchAsync(query);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var a = await _service.GetByIdAsync(id);
            if (a == null) return NotFound();
            return Ok(a);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAnimalDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPost("{id}/photo")]
        public async Task<IActionResult> UploadPhoto(Guid id)
        {
            var file = Request.Form.Files.Count > 0 ? Request.Form.Files[0] : null;
            await _service.UploadPhotoAsync(id, file);
            return NoContent();
        }
    }
}
