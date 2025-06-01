using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeDoSoftware.Application.DTOs.UserDTOs;
using WeDoSoftware.Domain.Entities;

namespace WeDoSoftware.Application.ServiceInterfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateUserDto dto);
        Task UpdateAsync(int id, UpdateUserDto dto);
        Task DeleteAsync(int id);
        Task<User?> ValidateUserAsync(string email, string password);
    }
}
