using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeDoSoftware.Domain.Entities;
using WeDoSoftware.Domain.RepositoryInterfaces;
using WeDoSoftware.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace WeDoSoftware.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TrainingTrackerDbContext _context;
        public UserRepository(TrainingTrackerDbContext context) 
        {
            _context = context;       
        }
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
        }
    }
}
