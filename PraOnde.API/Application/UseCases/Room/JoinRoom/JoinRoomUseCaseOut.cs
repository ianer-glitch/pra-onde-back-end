namespace PraOnde.API.Application.UseCases.Room.JoinRoom;

public class JoinRoomUseCaseOut
{
    public required string Username { get; set; }
    public required Guid RoomId { get; set; }
}