using System.Security.Claims;
using Api.Data;
using Api.Enums;
using Api.Models;
using Api.Services;
using Api.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

    

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Study>>> GetAllReviews()
    {
        var IdUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var reviews = await _studyReviewService.GetAllStudyReviewsAsync();

        var retorno = reviews.Where(p => p.Study.IdUser == IdUser && p.IdStudyPC == (int)StudyEnum.Registered && p.OperationDate <= DateTime.Now);


        return Ok(retorno);
    }
}
