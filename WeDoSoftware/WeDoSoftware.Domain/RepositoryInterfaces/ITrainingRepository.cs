using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeDoSoftware.Domain.Entities;

namespace WeDoSoftware.Domain.RepositoryInterfaces
{
    public interface ITrainingSessionRepository
    {
        Task<Training?> GetByIdAsync(int id);
        Task<IEnumerable<Training>> GetByUserAndMonthAsync(int userId, int year, int month);
        Task AddAsync(Training session);
        Task UpdateAsync(Training session);
        Task DeleteAsync(int id);
    }
}
