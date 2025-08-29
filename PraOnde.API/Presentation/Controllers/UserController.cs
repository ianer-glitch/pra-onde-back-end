using Microsoft.AspNetCore.Mvc;
using PraOnde.API.Application.Common;
using PraOnde.API.Application.UseCases.User.CreateUser;

namespace PraOnde.API.Presentation.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class UserController : Controller
{
    private readonly ILogger<RoomController> _logger;
    private readonly ICreateUserUseCase _createUserUseCase;
    public UserController(ILogger<RoomController> logger, ICreateUserUseCase createUserUseCase)
    {
        _logger = logger;
        _createUserUseCase = createUserUseCase;
    }
    
    [HttpPost]
    public async Task<ActionResult<Result<CreateUserUseCaseOut>>> CreateUser(CreateUserUseCaseIn request)
    {
        _logger.LogInformation($"Received request to create a user with Name: {request.Username}");

        var result = await _createUserUseCase.ExecuteAsync(request);

        if (result.IsSuccess)
        {
            _logger.LogInformation("User created successfully with Id: {UserId}", result.Value.UserId);
            return Ok(result);
        }

        _logger.LogWarning("Failed to create room. Errors: {Errors}", result.Error);
        return StatusCode(500);
    }
}