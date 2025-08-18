using ChatApp.Application.Interfaces;
using ChatApp.Infrastructure.Model;
using ChatApp.Domain.Entities;
using MongoDB.Driver;
using MongoDB.Bson;
using ChatApp.Domain.ValueObjects;
using AutoMapper;
using ChatApp.Application.Dtos;
using System.Text.Json;


namespace ChatApp.Infrastructure.Persistence;

public class ChatRoomRepository : IChatRoomRepository
{
    private readonly IMongoCollection<ChatRoomModel> _chatRooms;
    private readonly IMapper _mapper;

    public ChatRoomRepository(IMongoDatabase database, IMapper mapper)
    {
        _chatRooms = database.GetCollection<ChatRoomModel>("ChatRooms");
        _mapper = mapper;

    }

    public async Task<MongoId> Create(CreateChatRoom chatRoom)
    {
        var model = new ChatRoomModel
        {
            Title = chatRoom.Title.Value,
            CreatorId = chatRoom.CreatorId.Value
        };
        await _chatRooms.InsertOneAsync(model);
        return MongoId.MkUnsafe(model._id);
    }

    public async Task<ChatRoom?> FindOneById(MongoId id)
    {
        var model = await _chatRooms.Find(u => u._id == id.Value).FirstOrDefaultAsync();

        if (model is null)
        {
            return null;
        }

        return ChatRoom.MkUnsafe(model._id, model.Title, model.CreatorId, model.CreatedAt);
    }


    public async Task<(List<ChatRoomResDto>, NonNegativeNumber TotalCount)> Pagination(PositiveNumber pageNumber, PositiveNumber pageSize)
    {
        long totalCount = await _chatRooms.CountDocumentsAsync(Builders<ChatRoomModel>.Filter.Empty);

        var chatRooms = await _chatRooms.Find(Builders<ChatRoomModel>.Filter.Empty)
            .Skip((pageNumber.Value - 1) * pageSize.Value)
            .Limit(pageSize.Value)
            .ToListAsync();

        List<ChatRoomResDto> chatRoomList = _mapper.Map<List<ChatRoomResDto>>(chatRooms);

        return (chatRoomList, NonNegativeNumber.MkUnsafe(totalCount));
    }
}