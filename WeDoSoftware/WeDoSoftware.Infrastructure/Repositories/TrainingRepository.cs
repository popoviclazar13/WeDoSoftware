using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeDoSoftware.Domain.Entities;
using WeDoSoftware.Domain.RepositoryInterfaces;
using WeDoSoftware.Infrastructure.Data;

namespace WeDoSoftware.Infrastructure.Repositories
{
    public class TrainingRepository : ITrainingSessionRepository
    {
        private readonly TrainingTrackerDbContext _context;
        public TrainingRepository(TrainingTrackerDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Training session)
        {
            await _context.Trainings.AddAsync(session);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var session = await _context.Trainings.FindAsync(id);
            if (session != null)
            {
                _context.Trainings.Remove(session);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Training>> GetAllAsync()
        {
            return await _context.Trainings.ToListAsync();
        }

        public async Task<IEnumerable<Training>> GetAllByUserIdAsync(int userId)
        {
            return await _context.Trainings
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        public async Task<Training?> GetByIdAsync(int id)
        {
            return await _context.Trainings.FindAsync(id);
        }

        public async Task<IEnumerable<Training>> GetByUserAndMonthAsync(int userId, int year, int month)
        {
            return await _context.Trainings
                .Where(t => t.UserId == userId &&
                            t.DateTime.Year == year &&
                            t.DateTime.Month == month)
                .ToListAsync();
        }

        public async Task UpdateAsync(Training session)
        {
            _context.Trainings.Update(session);
            await _context.SaveChangesAsync();
        }
    }
}
