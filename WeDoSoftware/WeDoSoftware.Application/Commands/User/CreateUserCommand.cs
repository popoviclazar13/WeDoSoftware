using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using WeDoSoftware.Application.DTOs.UserDTOs;

public class CreateUserCommand : IRequest<int>
{
    public CreateUserDto CreateUserDto { get; }

    public CreateUserCommand(CreateUserDto createUserDto)
    {
        CreateUserDto = createUserDto;
    }
}
