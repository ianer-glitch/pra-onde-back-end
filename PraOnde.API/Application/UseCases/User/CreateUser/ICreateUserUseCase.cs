using PraOnde.API.Application.Common;

namespace PraOnde.API.Application.UseCases.User.CreateUser;

public interface ICreateUserUseCase
{
    Task<Result<CreateUserUseCaseOut>> ExecuteAsync(CreateUserUseCaseIn req);
}