using ChatApp.Application.Dtos;
using ChatApp.Domain.Entities;
using ChatApp.Domain.ValueObjects;

namespace ChatApp.Application.Interfaces;

public interface IChatRoomRepository
{
    Task<MongoId> Create(CreateChatRoom chatRoom);
    Task<ChatRoom?> FindOneById(MongoId id);

    Task<(List<ChatRoomResDto>, NonNegativeNumber TotalCount)> Pagination(PositiveNumber pageNumber, PositiveNumber pageSize);

}

