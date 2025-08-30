using FluentValidation;
using PraOnde.API.Application.Common;
using PraOnde.API.Domain.Exceptions;
using PraOnde.API.Infraestructure.Data.Repositories;
using PraOnde.API.Infraestructure.Data.UnitOfWork;

namespace PraOnde.API.Application.UseCases.User.CreateUser;

public class CreateUserUseCase : ICreateUserUseCase
{
    private readonly ILogger<CreateUserUseCase> _logger;
    private readonly IUnitOfWork _uow;
    
    public CreateUserUseCase(ILogger<CreateUserUseCase> logger, IUnitOfWork uow)
    {
        _logger = logger;
        _uow = uow;
        
    }

    public async Task<Result<CreateUserUseCaseOut>> ExecuteAsync(CreateUserUseCaseIn req)
    {
        try
        {
            _logger.LogInformation($"[CreateUserUseCase] Attempt to create user {req.Username}");

            var validator = new CreateUserUseCaseValidator();
            await validator.ValidateAndThrowAsync(req);

            var existingUser = await _uow.UserRepository.FirstOrDefaultAsync(f => f.Name.Equals(req.Username));
            if (existingUser != null)
            {
                _logger.LogInformation(
                    $"[CreateUserUseCase] Could not  create user {req.Username}, There's already a user with that name");
                throw new UserAlreadyExistException();
            }

            var newUser = new Domain.Entities.User(req.Username);

            await _uow.BeginTransactionAsync();
            
            await _uow.UserRepository.AddAsync(newUser);

            if (await _uow.CommitAsync() > 0)
            {
                _logger.LogInformation($"[CreateUserUseCase] Created user {req.Username}");
                return Result<CreateUserUseCaseOut>.Success(new CreateUserUseCaseOut()
                {
                    UserId = newUser.Id,
                });
            }

            throw new Exception();
        }
        catch (UserAlreadyExistException ex)
        {
            await _uow.RollbackAsync();
            _logger.LogError($"[CreateUserUseCase] {ex.Message},  stack trace: {ex.StackTrace}");
            return Result<CreateUserUseCaseOut>.Fail($"Já existe um usuário cadastrado com o nome {req.Username}");
        }
        catch (ValidationException ex)
        {
            await _uow.RollbackAsync();
            _logger.LogError($"[CreateUserUseCase] {ex.Message},  stack trace: {ex.StackTrace}");
            return Result<CreateUserUseCaseOut>.Fail(ex.Message);
        }
        catch (Exception ex)
        {
            await _uow.RollbackAsync();
            _logger.LogError(
                $"[CreateUserUseCase] Ocurred an error while attempting to create a new user: {ex.Message},  stack trace: {ex.StackTrace}");
            return Result<CreateUserUseCaseOut>.Fail("Ocorreu um erro desconhecido e não foi possível criar o usuário");
        }
    }
}