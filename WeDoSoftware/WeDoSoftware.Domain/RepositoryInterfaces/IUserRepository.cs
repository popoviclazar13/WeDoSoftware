﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeDoSoftware.Domain.Entities;

namespace WeDoSoftware.Domain.RepositoryInterfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByUsernameAsync(string username);
        Task AddAsync(User user);
        Task DeleteAsync(int id);
        Task UpdateAsync(User user);
        Task<User?> GetByEmailAsync(string email);
    }
}
