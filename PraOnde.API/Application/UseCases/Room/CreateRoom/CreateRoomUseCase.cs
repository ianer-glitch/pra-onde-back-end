using PraOnde.API.Infraestructure.Data;

namespace PraOnde.API.Application.UseCases.Room.CreateRoom;

public class CreateRoomUseCase : ICreateRoomUseCase
{
    private readonly ILogger<CreateRoomUseCaseIn> _logger;
    private readonly Context _context;
    public CreateRoomUseCase(ILogger<CreateRoomUseCaseIn> logger, Context context)
    {
        _logger = logger;
        _context = context;
    }
    public async Task<CreateRoomUseCaseOut> ExecuteAsync(CreateRoomUseCaseIn request)
    {
        try
        {
            var room = _context.Rooms.FirstOrDefault(r => r.Name == request.RoomName);
            if (room != null)
            {
                //AlreadyExistException
                throw new Exception();
            }

           await _context.Rooms.AddAsync(new Domain.Entities.Room(request.RoomName));
           if (await _context.SaveChangesAsync() > 0)
           {
               //Result.SetSucces()
               return new CreateRoomUseCaseOut
               {
                   RoomId = room.Id
               };
           }

           throw new Exception();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}