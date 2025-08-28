using Microsoft.AspNetCore.SignalR;
using PraOnde.API.Application.UseCases.Room.JoinRoom;

namespace PraOnde.API.Presentation.Hubs;

public class RoomHub : Hub , IRoomHub
{
    private readonly IHubContext<RoomHub> _hubContext;   
    
    public RoomHub(IHubContext<RoomHub>  hubContext)
    {
        _hubContext = hubContext;
        
    }

    private async Task SendMessageExample(Guid userId, string message)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveMessage", userId, message);   
    }


    public Task SendMessage(Guid roomId, string message)
    {
        throw new NotImplementedException();
    }

    public Task RecivesMessage(Guid roomId, string message)
    {
        throw new NotImplementedException();
    }

    public async Task JoinRoom(Guid roomId, Guid userId)
    {
        // var result = await _joinRoomUseCase.ExecuteAsync(new JoinRoomUseCaseIn
        // {
        //     RoomId = roomId,
        //     UserId = userId
        // });
        //
        // await Groups.AddToGroupAsync(Context.ConnectionId, result.RoomId.ToString());
        //
        // await Clients.Group(roomId.ToString()).SendAsync("UserJoined", result.Username);
        
    }

    public Task LeaveRoom(Guid roomId, Guid userId)
    {
        throw new NotImplementedException();
    }
}