namespace PraOnde.API.Application.UseCases.Room.CreateRoom;

public interface ICreateRoomUseCase
{
    Task<CreateRoomUseCaseOut> ExecuteAsync(CreateRoomUseCaseIn request);
}