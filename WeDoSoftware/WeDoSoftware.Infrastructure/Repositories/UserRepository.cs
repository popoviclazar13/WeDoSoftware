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

        public async Task DeleteAsync(int id)
        {
            var session = await _context.Users.FindAsync(id);
            if (session != null)
            {
                _context.Users.Remove(session);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
