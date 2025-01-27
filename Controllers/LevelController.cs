
using Api.Helpers.Extensions;
using Api.Models;
using Api.Models.Dtos;
using Api.Repository.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LevelController : ControllerBase
    {
        private readonly ILevelService _service;

        public LevelController(ILevelService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Level>>> GetAllLevels()
        {
            var levels = await _service.GetAllLevelsAsync();
            return Ok(levels);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Level>> GetLevelById(int id)
        {
            var level = await _service.GetLevelByIdAsync(id);
            if (level == null) return NotFound();
            return Ok(level);
        }

        [HttpPost]
        public async Task<ActionResult> AddLevel([FromBody] LevelDto level)
        {
            await _service.AddLevelAsync(level.ToLevel());
            return CreatedAtAction(nameof(GetLevelById), new { id = level.Id }, level);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateLevel(int id, [FromBody] Level level)
        {
            if (id != level.Id) return BadRequest();
            await _service.UpdateLevelAsync(level);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLevel(int id)
        {
            await _service.DeleteLevelAsync(id);
            return NoContent();
        }
    }
}
