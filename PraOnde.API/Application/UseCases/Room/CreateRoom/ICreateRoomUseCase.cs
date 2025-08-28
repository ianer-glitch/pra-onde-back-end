using PraOnde.API.Application.Common;

namespace PraOnde.API.Application.UseCases.Room.CreateRoom;

public interface ICreateRoomUseCase
{
    Task<Result<CreateRoomUseCaseOut>> ExecuteAsync(CreateRoomUseCaseIn request);
}