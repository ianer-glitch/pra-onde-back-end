using FluentValidation;

namespace PraOnde.API.Application.UseCases.Room.CreateRoom;

public class CreateRoomUseCaseValidator : AbstractValidator<CreateRoomUseCaseIn>
{
    public CreateRoomUseCaseValidator()
    {
        RuleFor(r => r.RoomName).NotNull().NotEmpty();
    }
}