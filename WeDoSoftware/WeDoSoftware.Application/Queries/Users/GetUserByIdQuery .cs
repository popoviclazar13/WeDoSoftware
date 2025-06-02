using MediatR;
using WeDoSoftware.Application.DTOs.UserDTOs;

public class GetUserByIdQuery : IRequest<UserDto>
{
    public int Id { get; }

    public GetUserByIdQuery(int id)
    {
        Id = id;
    }
}
