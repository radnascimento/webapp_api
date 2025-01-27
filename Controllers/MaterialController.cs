using Api.Helpers.Extensions;
using Api.Models;
using Api.Models.Dtos;
using Api.Repository.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialService _service;

        public MaterialController(IMaterialService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Material>>> GetAllMaterials()
        {
            var materials = await _service.GetAllMaterialsAsync();
            return Ok(materials);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Material>> GetMaterialById(int id)
        {
            var material = await _service.GetMaterialByIdAsync(id);
            if (material == null) return NotFound();
            return Ok(material);
        }

        [HttpPost]
        public async Task<ActionResult> AddMaterial([FromBody] MaterialDto material)
        {
            

            await _service.AddMaterialAsync(material.ToMaterial());
            return CreatedAtAction(nameof(GetMaterialById), new { id = material.IdMaterial }, material);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMaterial(int id, [FromBody] Material material)
        {
            if (id != material.IdMaterial) return BadRequest();
            await _service.UpdateMaterialAsync(material);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMaterial(int id)
        {
            await _service.DeleteMaterialAsync(id);
            return NoContent();
        }
    }
}
