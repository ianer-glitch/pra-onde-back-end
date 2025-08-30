using System.Transactions;
using Microsoft.EntityFrameworkCore.Storage;
using PraOnde.API.Domain.Entities;
using PraOnde.API.Infraestructure.Data.Repositories;

namespace PraOnde.API.Infraestructure.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly Context _context;
    private IDbContextTransaction _transaction;
    private IRepository<User> _userRepository;
    private IRepository<Room> _roomRepository;
    
    public UnitOfWork(Context context, IRepository<User> userRepository, IRepository<Room> roomRepository)
    {
        _context = context;
        _userRepository = userRepository;
        _roomRepository = roomRepository;
    }

    public IRepository<User> UserRepository => _userRepository ?? throw new NullReferenceException(nameof(UserRepository));
    public IRepository<Room> RoomRepository => _roomRepository ?? throw new NullReferenceException(nameof(RoomRepository));
    
    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
        return _transaction;
    }

    public Task<int> CommitAsync()
    {
        return _context.SaveChangesAsync();
    }

    public async Task RollbackAsync()
    {
        await _transaction.RollbackAsync();
    }
}