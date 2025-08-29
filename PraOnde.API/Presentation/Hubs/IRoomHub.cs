using PraOnde.API.Application.UseCases.Room.JoinRoom;

namespace PraOnde.API.Presentation.Hubs;

public interface IRoomHub
{
    Task SendMessage(Guid roomId, string message);
    Task RecivesMessage(Guid roomId, string message);
    Task JoinRoom(JoinRoomUseCaseIn req);
    Task LeaveRoom(Guid roomId,Guid userId);    
}