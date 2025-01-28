using Api.Data;
using Api.Models;
using Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository
{
    public class StudyRepository : IStudyRepository
    {
        private readonly ApplicationDbContext _context;

        public StudyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Study> GetStudyByIdAsync(int id)
        {
            return await _context.Studies
                .Include(s => s.Topic) // Include Topic data
                .FirstOrDefaultAsync(s => s.IdStudy == id);
        }

        public async Task<IEnumerable<Study>> GetAllStudiesAsync()
        {
            return await _context.Studies
                .Include(s => s.Topic) // Include Topic data
                .OrderByDescending(s => s.IdStudy)
                .ToListAsync();
        }

        public async Task<IEnumerable<Study>> GetStudiesAsync(int? idStudy = null, int? idTopic = null, string note = null, DateTime? operationDate = null, int page = 1, int pageSize = 10, string idUser = null)
        {
            IQueryable<Study> query = _context.Studies
                .Include(s => s.Topic) // Include Topic data
                .OrderByDescending(s => s.IdStudy);

            // Apply filtering based on the provided parameters
            if (idStudy.HasValue)
            {
                query = query.Where(s => s.IdStudy == idStudy.Value);
            }

            if (idTopic.HasValue)
            {
                query = query.Where(s => s.IdTopic == idTopic.Value);
            }

            if (!string.IsNullOrEmpty(note))
            {
                query = query.Where(s => s.Note.Contains(note));
            }

            if (operationDate.HasValue)
            {
                query = query.Where(s => s.OperationDate.Date == operationDate.Value.Date);
            }

            if (!string.IsNullOrEmpty(idUser))
            {
                query = query.Where(s => s.IdUser.Contains(idUser));
            }
            

            // Apply pagination
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            // Execute the query and return the results
            return await query.ToListAsync();
        }


        public async Task<IEnumerable<Study>> GetStudiesByTopicIdAsync(int topicId)
        {
            return await _context.Studies
                .Where(s => s.IdTopic == topicId)
                .Include(s => s.Topic) // Include Topic data
                .ToListAsync();
        }

        public async Task AddStudyAsync(Study study)
        {
            await _context.Studies.AddAsync(study);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStudyAsync(Study study)
        {
            _context.Studies.Update(study);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStudyAsync(int id)
        {
            var study = await _context.Studies.FindAsync(id);
            if (study != null)
            {
                _context.Studies.Remove(study);
                await _context.SaveChangesAsync();
            }
        }
    }
}
