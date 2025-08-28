using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using PraOnde.API.Domain.Entities;

namespace PraOnde.API.Infraestructure.Data;

public class Context : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserRoom> UserRooms { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Pin> Pins { get; set; }
    public DbSet<Message> Messages { get; set; }
    
    public Context(DbContextOptions<Context> options) : base(options) { }
    
    
    
}