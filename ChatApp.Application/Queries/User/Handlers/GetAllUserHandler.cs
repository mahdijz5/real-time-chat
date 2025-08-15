
using AutoMapper;
using ChatApp.Application.Dtos;
using ChatApp.Application.Interfaces;
using ChatApp.Domain.ValueObjects;
using MediatR;

namespace ChatApp.Application.Queries.Users.Handlers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, PaginatedList<UserResDto>>
{

    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetAllUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<UserResDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        PositiveNumber pageNumber = PositiveNumber.MkUnsafe(request.PageNumber);
        PositiveNumber pageSize = PositiveNumber.MkUnsafe(request.PageSize);
        var (users, totalCount) = await _userRepository.Pagination(pageNumber, pageSize);

        return new PaginatedList<UserResDto>(
            users,
            totalCount.Value,
            request.PageNumber,
            request.PageSize);
    }

}