namespace PraOnde.API.Application.UseCases.Room.JoinRoom;

public interface IJoinRoomUseCase
{
    Task<JoinRoomUseCaseOut> ExecuteAsync(JoinRoomUseCaseIn request);
}