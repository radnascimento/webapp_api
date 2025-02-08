using System.IO.Compression;
using System.Security.Claims;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using Api.Helpers.Extensions;
using Api.Models;
using Api.Models.Dtos;
using Api.Models.Filters;
using Api.Repository.Interface;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch.Operations;
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
            var IdUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            try
            {
                // Call the service to get studies based on the query parameters
                var studies = await _studyService.GetStudiesAsync(idStudy, idTopic, note, operationDate, page, pageSize, IdUser);

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


        /// <summary>
        /// 📌 Compressed JSON response using GZip
        /// </summary>
        [HttpGet("compressed")]
        public async Task<IActionResult> GetCompressedStudiesAsync([FromQuery] StudyFilter filter)
        {
            try
            {
                var idUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var studies = await _studyService.GetStudiesAsync(filter.IdStudy, filter.IdTopic, filter.Note, filter.OperationDate, filter.Page, filter.PageSize, idUser);

                if (studies == null || !studies.Any())
                {
                    return NotFound("No studies found with the specified filters.");
                }

                // Convert data to JSON
                //string json = JsonSerializer.Serialize(studies);

                string json = JsonSerializer.Serialize(studies, new JsonSerializerOptions
                {
                    ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles,
                    WriteIndented = false
                });

                byte[] jsonBytes = Encoding.UTF8.GetBytes(json);

                // Compress data using GZip
                using var compressedStream = new MemoryStream();
                using (var gzipStream = new GZipStream(compressedStream, CompressionMode.Compress, true))
                {
                    gzipStream.Write(jsonBytes, 0, jsonBytes.Length);
                }

                compressedStream.Seek(0, SeekOrigin.Begin);

                return File(compressedStream.ToArray(), "application/gzip", "studies.json.gz");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateStudyAsync(StudyDto study)
        {



            try
            {
                study.IdUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                await _studyService.AddStudyAsync(study.ToStudy());

                return Ok(study);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }


        }



        /// <summary>
        /// 📌 Receives a GZip-compressed JSON request
        /// </summary>
        [HttpPost("compressed")]
        public async Task<ActionResult> CreateCompressedStudyAsync()
        {
            try
            {
                // Read and decompress GZip request
                using var decompressedStream = new MemoryStream();
                using (var gzipStream = new GZipStream(Request.Body, CompressionMode.Decompress))
                {
                    await gzipStream.CopyToAsync(decompressedStream);
                }

                decompressedStream.Seek(0, SeekOrigin.Begin);
                using var reader = new StreamReader(decompressedStream, Encoding.UTF8);
                string json = await reader.ReadToEndAsync();

                // Deserialize JSON to object
                var study = JsonSerializer.Deserialize<StudyDto>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (study == null)
                {
                    return BadRequest("Invalid study data.");
                }

                study.IdUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                await _studyService.AddStudyAsync(study.ToStudy());

                return Ok(study);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpPut("{id}")]


        //public async Task<ActionResult> UpdateStudyAsync(int id, int idTopic, string note, string operationDate)
        public async Task<ActionResult> UpdateStudyAsync(int id, EditStudyDto study)
        {
            // Validate the ID
            if (id == 0)
            {
                return BadRequest("Invalid ID. ID must be greater than 0.");
            }

            // Validate the study DTO
            if (study == null)
            {
                return BadRequest("Study data is required.");
            }

            // Validate specific fields in the study DTO
            if (string.IsNullOrWhiteSpace(study.Note))
            {
                return BadRequest("Note is required.");
            }

            if (study.IdTopic <= 0)
            {
                return BadRequest("Invalid Topic ID. Topic ID must be greater than 0.");
            }

            if (study.OperationDate == default)
            {
                return BadRequest("Operation Date is required.");
            }

            // Assign the current user ID to the study
            study.IdUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Validate the user ID
            if (string.IsNullOrWhiteSpace(study.IdUser))
            {
                return BadRequest("User ID is missing or invalid.");
            }

            try
            {
               

                if (study.IdStudyPC == 0)
                {
                    var data = await _studyService.GetStudyByIdAsync(study.IdStudy);
                    if (data == null)
                    {
                        return NotFound(new { message = "Study not found." });
                    }

                    study.IdStudyPC = data.IdStudyPC;
                }

                await _studyService.UpdateStudyAsync(study.ToStudy());

                return Ok(new { message = "Study updated successfully.", study });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating study: {ex.Message}"); // Replace with a logging system
                return StatusCode(500, new { message = "An internal server error occurred." });
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudyAsync(int id)
        {
            await _studyService.DeleteStudyAsync(id);
            return NoContent();
        }
    }
}
