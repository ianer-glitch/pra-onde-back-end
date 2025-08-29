using FluentValidation;

namespace PraOnde.API.Application.UseCases.User.CreateUser;

public class CreateUserUseCaseValidator : AbstractValidator<CreateUserUseCaseIn>
{
    public CreateUserUseCaseValidator()
    {
        RuleFor(r=>r.Username).NotNull().NotEmpty();    
    }
}