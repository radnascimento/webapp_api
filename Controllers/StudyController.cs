using System.Threading.Tasks;
using Api.Helpers.Extensions;
using Api.Models;
using Api.Models.Dtos;
using Api.Models.Filters;
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
    public class StudyController : ControllerBase
    {
        private readonly IStudyService _studyService;

        public StudyController(IStudyService studyService)
        {
            _studyService = studyService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Study>> GetStudyByIdAsync(int id)
        {
            var study = await _studyService.GetStudyByIdAsync(id);
            if (study == null)
            {
                return NotFound();
            }
            return Ok(study);
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Study>>> GetAllStudiesAsync()
        //{
        //    var studies = await _studyService.GetAllStudiesAsync();
        //    return Ok(studies);
        //}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Study>>> GetAllStudiesAsync([FromQuery] StudyFilter filter)
        {
            // You can now access the parameters from the filter object
            var idStudy = filter.IdStudy;
            var idTopic = filter.IdTopic;
            var note = filter.Note;
            var operationDate = filter.OperationDate;
            var page = filter.Page;
            var pageSize = filter.PageSize;

            try
            {
                // Call the service to get studies based on the query parameters
                var studies = await _studyService.GetStudiesAsync(idStudy, idTopic, note, operationDate ,page, pageSize);

                if (studies == null || !studies.Any())
                {
                    return NotFound("No studies found with the specified filters.");
                }

                return Ok(studies); // Return the studies as a response with HTTP 200
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateStudyAsync(StudyDto study)
        {
            await _studyService.AddStudyAsync(study.ToStudy());

            return Ok(study);


            //return CreatedAtAction(nameof(GetStudyByIdAsync), new { id = study.IdStudy }, study);


        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStudyAsync(int id, Study study)
        {
            if (id != study.IdStudy)
            {
                return BadRequest();
            }

            await _studyService.UpdateStudyAsync(study);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudyAsync(int id)
        {
            await _studyService.DeleteStudyAsync(id);
            return NoContent();
        }
    }
}
