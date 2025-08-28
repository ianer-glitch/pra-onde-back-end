namespace PraOnde.API.Application.UseCases.Room.JoinRoom;

public class JoinRoomUseCaseIn
{
    public required Guid UserId { get; set; }
    public required Guid RoomId { get; set; }
}