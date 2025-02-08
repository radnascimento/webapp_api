using Api.Data;
using Api.Models;
using Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

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
            return await _context.Study
                .AsNoTracking()
                .Include(s => s.Topic) 
                .FirstOrDefaultAsync(s => s.IdStudy == id);
        }

        public async Task<IEnumerable<Study>> GetAllStudiesAsync()
        {
            return await _context.Study
                .Include(s => s.Topic) // Include Topic data
                .OrderByDescending(s => s.IdStudy)
                .ToListAsync();
        }

        public async Task<IEnumerable<Study>> GetStudiesAsync(int? idStudy = null, int? idTopic = null, string note = null, DateTime? operationDate = null, int page = 1, int pageSize = 10, string idUser = null)
        {
            IQueryable<Study> query = _context.Study
                .Include(s => s.Topic)
                .Include(s => s.StudyPC)
                .Include (s => s.StudyReviews)
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

            var lst = await query.ToListAsync();


            foreach (Study item in lst)
            {
                try
                {
                    // Validate the Note property
                    if (!string.IsNullOrEmpty(item.Note) && item.Note.Length > 20)
                    {
                        // Truncate the note to 20 characters and append "..."
                        item.Note = item.Note.Substring(0, 300) + "{seemoreinformation}";
                    }
                    else if (!string.IsNullOrEmpty(item.Note))
                    {
                        // If the note is shorter than 20 characters, keep it as is
                        item.Note = item.Note;
                    }
                    else
                    {
                        // If the note is null or empty, set a default value
                        item.Note = "No note available.";
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception instead of rethrowing it
                    // You can use a logging framework like Serilog, NLog, or the built-in ILogger
                    //Console.WriteLine($"An error occurred while processing Study ID {item.Id}: {ex.Message}");
                    // Optionally, you can continue processing the next item
                    continue;
                }
            }


            // Execute the query and return the results
            return lst;
        }


        public async Task<IEnumerable<Study>> GetStudiesByTopicIdAsync(int topicId)
        {
            return await _context.Study
                .Where(s => s.IdTopic == topicId)
                .Include(s => s.Topic) // Include Topic data
                .ToListAsync();
        }

        public async Task AddStudyAsync(Study study)
        {
            await _context.Study.AddAsync(study);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStudyAsync(Study study)
        {
            _context.Study.Update(study);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStudyAsync(int id)
        {
            var study = await _context.Study.FindAsync(id);
            if (study != null)
            {
                _context.Study.Remove(study);
                await _context.SaveChangesAsync();
            }
        }
    }
}
