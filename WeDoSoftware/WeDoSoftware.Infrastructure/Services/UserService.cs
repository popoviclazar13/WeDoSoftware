using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeDoSoftware.Application.DTOs.UserDTOs;
using WeDoSoftware.Application.Exceptions;
using WeDoSoftware.Application.ServiceInterfaces;
using WeDoSoftware.Domain.Entities;
using WeDoSoftware.Infrastructure.Data;

namespace WeDoSoftware.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly TrainingTrackerDbContext _context;
        private readonly IMapper _mapper;

        public UserService(TrainingTrackerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> CreateAsync(CreateUserDto dto)
        {
            var user = _mapper.Map<User>(dto);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new NotFoundException($"User with id {id} not found.");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                throw new NotFoundException($"User with ID {id} not found.");

            return _mapper.Map<UserDto>(user);
        }

        public async Task UpdateAsync(int id, UpdateUserDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new NotFoundException($"User with id {id} not found.");
            }

            _mapper.Map(dto, user);

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task<User?> ValidateUserAsync(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null || user.Password != password) 
                return null;

            return user;
        }
    }
}
