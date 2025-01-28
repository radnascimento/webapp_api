using Api.Data;
using Api.Models;
using Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository
{
    public class TopicRepository: ITopicRepository
    {
        private readonly ApplicationDbContext _context;
        public TopicRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Topic>> GetAllTopicsAsync()
        {
            return await _context.Topics.OrderBy(s => s.Name).ToListAsync();
                


        }

        public async Task<Topic> GetTopicByIdAsync(int id)
        {
            return await _context.Topics.FindAsync(id);
        }

        public async Task AddTopicAsync(Topic topic)
        {
            await _context.Topics.AddAsync(topic);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTopicAsync(Topic topic)
        {
            _context.Topics.Update(topic);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTopicAsync(int id)
        {
            var topic = await _context.Topics.FindAsync(id);
            if (topic != null)
            {
                _context.Topics.Remove(topic);
                await _context.SaveChangesAsync();
            }
        }
    }
}
