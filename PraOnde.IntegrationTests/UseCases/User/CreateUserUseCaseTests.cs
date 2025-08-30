using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using Moq;
using PraOnde.API.Application.UseCases.User.CreateUser;
using PraOnde.API.Domain.Exceptions;
using PraOnde.API.Infraestructure.Data.UnitOfWork;

namespace PraOnde.IntegrationTests.UseCases.User;

public class CreateUserUseCaseTests
{
    
    [Fact]
    public async Task ShouldCreateUser()
    {
        var username = "newUser";
        
        var mockLogger = new Mock<ILogger<CreateUserUseCase>>();
        var mockUow = new Mock<IUnitOfWork>();

        mockUow.Setup(s => s.UserRepository.FirstOrDefaultAsync(It.IsAny<Expression<Func<API.Domain.Entities.User,bool>>>())).ReturnsAsync((API.Domain.Entities.User?)null);
        mockUow.Setup(s => s.CommitAsync()).ReturnsAsync(1);
        
        var createUserUseCase = new CreateUserUseCase(mockLogger.Object,mockUow.Object);

        var result = await createUserUseCase.ExecuteAsync(new CreateUserUseCaseIn()
        {
            Username = username
        });
        
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
    }

    [Fact] public async Task WhenNameHasAlreadyBeenRegistered_ShoudThrowUserAlreadyExistsException()
    {
        var username = "existingUser";
        var existingUser = new API.Domain.Entities.User(username);
        
        var mockLogger = new Mock<ILogger<CreateUserUseCase>>();
        var mockUow = new Mock<IUnitOfWork>();

        mockUow.Setup(s => s.UserRepository.FirstOrDefaultAsync(It.IsAny<Expression<Func<API.Domain.Entities.User,bool>>>())).ReturnsAsync(existingUser);
        mockUow.Setup(s => s.CommitAsync()).ReturnsAsync(0);
        
        var createUserUseCase = new CreateUserUseCase(mockLogger.Object,mockUow.Object);

        var result = await createUserUseCase.ExecuteAsync(new CreateUserUseCaseIn()
        {
            Username = username
        });
        
        Assert.False(result.IsSuccess);
    
    }
}