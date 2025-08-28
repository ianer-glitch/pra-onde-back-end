using PraOnde.API.Domain.Entities;
using PraOnde.API.Infraestructure.Data;

namespace PraOnde.API.Application.UseCases.Room.JoinRoom;

public class JoinRoomUseCase : IJoinRoomUseCase
{
    private readonly Context _context;
    private ILogger<JoinRoomUseCase> _logger;   
    public JoinRoomUseCase(Context context, ILogger<JoinRoomUseCase> logger)
    {
        _context = context;
        _logger = logger;
    }


    public async Task<JoinRoomUseCaseOut> ExecuteAsync(JoinRoomUseCaseIn request)
    {
        try
        {
            var room = await _context.Rooms.FindAsync(request.RoomId);
            if (room == null)
            {
                _logger.LogWarning($"No room found with id {request.RoomId}");
                throw new ArgumentException(nameof(request.RoomId));
            }
            
            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null)
            {
                _logger.LogWarning($"No user found with id {request.UserId}");
                throw new ArgumentException(nameof(request.UserId));
            }

            await _context.AddAsync(new UserRoom(user.Id, room.Id));
            return new JoinRoomUseCaseOut
            {
                RoomId = room.Id,
                Username = user.Name    
            };
        }
        catch (Exception e)
        {
            _logger.LogWarning($"User with id {request.UserId} could not join {request.RoomId}: {e.Message}, {e.StackTrace}, {e.InnerException}");
            throw;
        }
    }
}