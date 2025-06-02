using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeDoSoftware.Application.DTOs.UserDTOs;

namespace WeDoSoftware.Application.Commands.User
{
    public record UpdateUserCommand(int Id, UpdateUserDto Dto) : IRequest;
}
