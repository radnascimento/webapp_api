using Api.Helpers.Extensions;
using Api.Models;
using Api.Models.Dtos;
using Api.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ApplicationConfigController : ControllerBase
    {
        private readonly IApplicationConfigService _service;

        // Inject the service via constructor
        public ApplicationConfigController(IApplicationConfigService service)
        {
            _service = service;
        }

        // GET: api/ApplicationConfig
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationConfig>>> GetAllApplicationConfigs()
        {
            try
            {
                var configs = await _service.GetAllApplicationConfigsAsync();
                return Ok(configs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/ApplicationConfig/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationConfig>> GetApplicationConfigById(int id)
        {
            var config = await _service.GetApplicationConfigByIdAsync(id);
            if (config == null) return NotFound();
            return Ok(config);
        }

        // POST: api/ApplicationConfig
        [HttpPost]
        public async Task<ActionResult> AddApplicationConfig([FromBody] ApplicationConfigDto configDto)
        {
            var config = configDto.ToApplicationConfig();
            await _service.AddApplicationConfigAsync(config);
            return CreatedAtAction(nameof(GetApplicationConfigById), new { id = config.IdApplicationConfig }, config);
        }

        // PUT: api/ApplicationConfig/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateApplicationConfig(int id, [FromBody] ApplicationConfig config)
        {
            if (id != config.IdApplicationConfig) return BadRequest();
            await _service.UpdateApplicationConfigAsync(config);
            return NoContent();
        }

        // DELETE: api/ApplicationConfig/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteApplicationConfig(int id)
        {
            await _service.DeleteApplicationConfigAsync(id);
            return NoContent();
        }
    }
}