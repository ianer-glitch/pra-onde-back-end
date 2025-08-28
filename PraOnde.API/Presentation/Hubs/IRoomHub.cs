namespace PraOnde.API.Presentation.Hubs;

public interface IRoomHub
{
    Task SendMessage(Guid roomId, string message);
    Task RecivesMessage(Guid roomId, string message);
    Task JoinRoom(Guid roomId,Guid userId);
    Task LeaveRoom(Guid roomId,Guid userId);    
}