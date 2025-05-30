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

        public Task AddAsync(Training session)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Training?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Training>> GetByUserAndMonthAsync(int userId, int year, int month)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Training session)
        {
            throw new NotImplementedException();
        }
    }
}
