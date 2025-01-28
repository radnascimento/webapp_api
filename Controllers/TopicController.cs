using System.Security.Claims;
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
    public class TopicController : ControllerBase
    {
        private readonly ITopicService _service;

        public TopicController(ITopicService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Topic>>> GetAllTopics()
        {
            var topics = await _service.GetAllTopicsAsync();

            var IdUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(topics.Where(p=> p.IdUser == IdUser));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Topic>> GetTopicById(int id)
        {
            var topic = await _service.GetTopicByIdAsync(id);
            if (topic == null) return NotFound();
            return Ok(topic);
        }

        [HttpPost]
        public async Task<ActionResult> AddTopic([FromBody] TopicDto topic)
        {
            topic.IdUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _service.AddTopicAsync(topic.ToTopic());

            return CreatedAtAction(nameof(GetTopicById), new { id = topic.Id }, topic);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTopic(int id, [FromBody] Topic topic)
        {
            if (id != topic.Id) return BadRequest();
            await _service.UpdateTopicAsync(topic);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTopic(int id)
        {
            await _service.DeleteTopicAsync(id);
            return NoContent();
        }
    }
}
