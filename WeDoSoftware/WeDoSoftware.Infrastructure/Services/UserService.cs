using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeDoSoftware.Application.DTOs.UserDTOs;
using WeDoSoftware.Application.ServiceInterfaces;
using WeDoSoftware.Domain.Entities;
using WeDoSoftware.Infrastructure.Data;

namespace WeDoSoftware.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly TrainingTrackerDbContext _context;

        public UserService(TrainingTrackerDbContext context)
        {
            _context = context;
        }
        public async Task<int> CreateAsync(CreateUserDto dto)
        {
            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                Password = dto.Password 
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with id {id} not found.");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            return await _context.Users.Select(x => new UserDto
            {
                Id = x.Id,
                Username = x.Username,
                Email = x.Email

            })
              .ToListAsync();
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with id {id} not found.");
            }

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password
            };
        }

        public async Task UpdateAsync(int id, UpdateUserDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with id {id} not found.");
            }

            user.Username = dto.Username;
            user.Email = dto.Email;

            if (!string.IsNullOrEmpty(dto.Password))
            {
                user.Password = dto.Password; // Opet, hashovati u pravoj aplikaciji
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
