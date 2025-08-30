using Microsoft.EntityFrameworkCore.Storage;
using PraOnde.API.Domain.Entities;
using PraOnde.API.Infraestructure.Data.Repositories;

namespace PraOnde.API.Infraestructure.Data.UnitOfWork;

public interface IUnitOfWork
{
    IRepository<User> UserRepository { get; }
    IRepository<Room> RoomRepository { get; }
    Task<int> CommitAsync();
}