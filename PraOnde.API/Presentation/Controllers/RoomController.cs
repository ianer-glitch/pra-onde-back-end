using Microsoft.AspNetCore.Mvc;
using PraOnde.API.Application.Common;
using PraOnde.API.Application.UseCases.Room.CreateRoom;

namespace PraOnde.API.Presentation.Controllers;
[ApiController]
[Route("api/[controller]/")]
public class RoomController : Controller
{
    private readonly ILogger<RoomController> _logger;
    private readonly ICreateRoomUseCase _createRoomUseCase;
    public RoomController(ILogger<RoomController> logger, ICreateRoomUseCase createRoomUseCase)
    {
        _logger = logger;
        _createRoomUseCase = createRoomUseCase;
    }

    [HttpPost]
    public async Task<ActionResult<Result<CreateRoomUseCaseOut>>> CreateRoom(CreateRoomUseCaseIn request)
    {
        _logger.LogInformation("Received request to create a room with Name: {RoomName}", request.RoomName);

        var result = await _createRoomUseCase.ExecuteAsync(request);

        if (result.IsSuccess)
        {
            _logger.LogInformation("Room created successfully with Id: {RoomId}", result.Value.RoomId);
            return Ok(result);
        }

        _logger.LogWarning("Failed to create room. Errors: {Errors}", result.Error);
        return StatusCode(500);
    }
}