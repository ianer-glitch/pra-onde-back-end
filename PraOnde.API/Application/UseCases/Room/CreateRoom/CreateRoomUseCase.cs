using FluentValidation;
using PraOnde.API.Application.Common;
using PraOnde.API.Domain.Exceptions;
using PraOnde.API.Infraestructure.Data;
using PraOnde.API.Infraestructure.Data.Repositories;

namespace PraOnde.API.Application.UseCases.Room.CreateRoom;

public class CreateRoomUseCase : ICreateRoomUseCase
{
    private readonly ILogger<CreateRoomUseCaseIn> _logger;
    private readonly IRepository<Domain.Entities.Room> _roomRepository;
    private Guid _logContextId = Guid.NewGuid();
    public CreateRoomUseCase(ILogger<CreateRoomUseCaseIn> logger, IRepository<Domain.Entities.Room> roomRepository)
    {
        _logger = logger;
        _roomRepository = roomRepository;
    }
    public async Task<Result<CreateRoomUseCaseOut>> ExecuteAsync(CreateRoomUseCaseIn request)
    {
        try
        {
            _logger.LogInformation($"[CreateRoomUseCase] Initializing for Room {request.RoomName}");

            var validator = new CreateRoomUseCaseValidator();
            await validator.ValidateAndThrowAsync(request);

            var room = await _roomRepository.FirstOrDefaultAsync(r => r.Name == request.RoomName);
            if (room != null)
            {
                _logger.LogWarning($"[CreateRoomUseCase] Room with name {request.RoomName} already exists");
                throw new RoomAlreadyExistException();
            }

            await _roomRepository.AddAsync(new Domain.Entities.Room(request.RoomName));
            if (await _roomRepository.SaveChangesAsync() > 0)
            {
                _logger.LogInformation($"[CreateRoomUseCase] Room {request.RoomName} was successfully created");
                return Result<CreateRoomUseCaseOut>.Success(new CreateRoomUseCaseOut
                {
                    RoomId = room.Id
                });
            }

            _logger.LogInformation($"[CreateRoomUseCase] Room {request.RoomName} could not be created");
            throw new Exception($"[CreateRoomUseCase] Room {request.RoomName} could not be created");
        }
        catch (RoomAlreadyExistException e)
        {
            _logger.LogError($"[CreateRoomUseCase]{e.Message},Exception: {e.Message},InnerException: {e.InnerException}");
            return Result<CreateRoomUseCaseOut>.Fail("Uma sala com esse nome já existe");
        }
        catch (ValidationException e)
        {
            _logger.LogError($"[CreateRoomUseCase]{e.Message},Exception: {e.Message},InnerException: {e.InnerException}");
            return Result<CreateRoomUseCaseOut>.Fail($"{e.Message}");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Result<CreateRoomUseCaseOut>.Fail($"Houve um erro desconhecido!, informe o código {_logContextId} ao administrador");
        }
    }
}