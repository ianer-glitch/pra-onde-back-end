using Microsoft.AspNetCore.SignalR;
using PraOnde.API.Application.UseCases.Room.JoinRoom;

namespace PraOnde.API.Presentation.Hubs;

public class RoomHub : Hub , IRoomHub
{
    private readonly IHubContext<RoomHub> _hubContext;  
    private readonly IServiceProvider _serviceProvider; 
    
    public RoomHub(IHubContext<RoomHub>  hubContext, IServiceProvider serviceProvider)
    {
        _hubContext = hubContext;
        _serviceProvider = serviceProvider;
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

    public async Task JoinRoom(JoinRoomUseCaseIn request)
    {
        var scope =  _serviceProvider.CreateScope();
        var joinUseCase = scope.ServiceProvider.GetRequiredService<IJoinRoomUseCase>();
        var result = await joinUseCase.ExecuteAsync(request);
        
        await Groups.AddToGroupAsync(Context.ConnectionId, result.RoomId.ToString());
        
        await Clients.Group(result.RoomId.ToString()).SendAsync("UserJoined", result.Username);
        scope.Dispose();
    }

    public Task LeaveRoom(Guid roomId, Guid userId)
    {
        throw new NotImplementedException();
    }
}