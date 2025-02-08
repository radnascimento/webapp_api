using System.IO.Compression;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Api.Data;
using Api.Enums;
using Api.Helpers.Extensions;
using Api.Models;
using Api.Models.Dtos;
using Api.Repository.Interface;
using Api.Services;
using Api.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ReviewsController : ControllerBase
{
    private readonly IStudyReviewService _studyReviewService;

    public ReviewsController(IStudyReviewService studyReviewService)
    {
        _studyReviewService = studyReviewService;
    }

    [HttpGet("compressed")]
    public async Task<ActionResult<IEnumerable<Study>>> GetCompressedAllReviews()
    {
        var IdUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var reviews = await _studyReviewService.GetAllStudyReviewsAsync();

        var retorno = reviews.Where(p => p.Study.IdUser == IdUser && p.IdStudyPC == (int)StudyEnum.Registered && p.OperationDate.Date <= DateTime.Now.Date);

        string json = JsonSerializer.Serialize(retorno, new JsonSerializerOptions
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

        return File(compressedStream.ToArray(), "application/gzip", "reviews.json.gz");


    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Study>>> GetAllReviews()
    {
        var IdUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var reviews = await _studyReviewService.GetAllStudyReviewsAsync();

        var retorno = reviews.Where(p => p.Study.IdUser == IdUser && p.IdStudyPC == (int)StudyEnum.Registered && p.OperationDate.Date <= DateTime.Now.Date);


        return Ok(retorno);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<object>> UpdateStudyReviewAsync(int id, EditStudyReviewDto study)
    {
        var IdUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var data = await _studyReviewService.GetStudyReviewByIdAsync(study.IdStudyReview);

        if (data == null)
        {
            return NotFound(new { message = "Study not found." });
        }

        study.IdStudyPC = (int)StudyEnum.Read;
        study.IdStudy = data.IdStudy;

        try
        {
            await _studyReviewService.UpdateStudyReviewAsync(study.ToStudy());
            return Ok(new { message = "Study updated successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while updating the study.", error = ex.Message });
        }
    }


    //[HttpPut("{id}")]
    //public async Task<ActionResult> UpdateStudyReviewAsync(int id, EditStudyReviewDto study)
    //{
    //    var IdUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    //    var data = await _studyReviewService.GetStudyReviewByIdAsync(study.IdStudyReview);

    //    if (data == null)
    //    {
    //        return NotFound(new { message = "Study not found." });
    //    }

    //    study.IdStudyPC = (int)StudyEnum.Read;
    //    study.IdStudy = data.IdStudy;

    //    try
    //    {
    //        await _studyReviewService.UpdateStudyReviewAsync(study.ToStudy());
    //    }
    //    catch (Exception ex)
    //    {

    //        throw;
    //    }

    //    return Ok();
    //}
}
