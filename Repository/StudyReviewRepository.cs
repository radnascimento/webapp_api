using Api.Data;
using Api.Enums;
using Api.Models;
using Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

public class StudyReviewRepository : IStudyReviewRepository
{
    private readonly ApplicationDbContext _context;

    public StudyReviewRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<StudyReview> GetStudyReviewByIdAsync(int id)
    {
        return await _context.StudyReview
            .AsNoTracking()
            .Include(sr => sr.Study)
            .Include(sr => sr.Study.Topic)
            .Include(sr => sr.StudyPC)
            .FirstOrDefaultAsync(sr => sr.IdStudyReview == id);
    }

    public async Task<IEnumerable<StudyReview>> GetAllStudyReviewsAsync()
    {
        try
        {
            var studyReviews = await _context.StudyReview
             .Include(sr => sr.Study)
             .Include(sr => sr.StudyPC)
             .Include(sr => sr.Study.Topic)
             .Where(sr => sr.IdStudyPC == (int)StudyEnum.Registered) // Apply filter first
             .GroupBy(sr => sr.IdStudy)
             .Select(g => g.OrderBy(sr => sr.OperationDate).FirstOrDefault()) // Select record with min OperationDate
             .ToListAsync();


            return studyReviews.OrderByDescending(s => s.IdStudyReview);
        }
        catch (Exception ex)
        {

            throw;
        }
    }


    public async Task<IEnumerable<StudyReview>> GetStudyReviewsAsync(
        int? idStudy = null,
        int? idStudyPC = null,
        DateTime? operationDate = null,
        int page = 1,
        int pageSize = 10)
    {
        IQueryable<StudyReview> query = _context.StudyReview
            .Include(sr => sr.Study)
            .Include(sr => sr.StudyPC)
            .OrderByDescending(sr => sr.IdStudyReview);

        // Apply filters
        if (idStudy.HasValue)
        {
            query = query.Where(sr => sr.IdStudy == idStudy.Value);
        }

        if (idStudyPC.HasValue)
        {
            query = query.Where(sr => sr.IdStudyPC == idStudyPC.Value);
        }

        if (operationDate.HasValue)
        {
            query = query.Where(sr => sr.OperationDate.Date == operationDate.Value.Date);
        }

        // Apply pagination
        return await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<IEnumerable<StudyReview>> GetStudyReviewsByStudyIdAsync(int studyId)
    {
        return await _context.StudyReview
            .Where(sr => sr.IdStudy == studyId)
            .Include(sr => sr.Study)
            .Include(sr => sr.StudyPC)
            .ToListAsync();
    }

    public async Task AddStudyReviewAsync(StudyReview studyReview)
    {
        await _context.StudyReview.AddAsync(studyReview);
        await _context.SaveChangesAsync();
    }

    public async Task AddStudyReviewsAsync(IEnumerable<StudyReview> studyReviews)
    {
        await _context.StudyReview.AddRangeAsync(studyReviews);
        await _context.SaveChangesAsync();
    }


    public async Task UpdateStudyReviewAsync(StudyReview studyReview)
    {
        try
        {
            _context.StudyReview.Update(studyReview);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public async Task DeleteStudyReviewAsync(int id)
    {
        var studyReview = await _context.StudyReview.FindAsync(id);
        if (studyReview != null)
        {
            _context.StudyReview.Remove(studyReview);
            await _context.SaveChangesAsync();
        }
    }
}
