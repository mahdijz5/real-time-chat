
using ChatApp.Application.Dtos;
using MediatR;

namespace ChatApp.Application.Queries.Users;

public class GetAllUsersQuery : IRequest<PaginatedList<UserResDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}