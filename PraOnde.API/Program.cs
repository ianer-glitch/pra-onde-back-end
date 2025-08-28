using Microsoft.EntityFrameworkCore;
using PraOnde.API.Application.UseCases.Room.JoinRoom;
using PraOnde.API.Infraestructure.Data;
using PraOnde.API.Presentation.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Context>(o => o.UseInMemoryDatabase(("praOndeDb")));

builder.Services.AddSingleton<IRoomHub, RoomHub>();

builder.Services.AddScoped<IJoinRoomUseCase, JoinRoomUseCase>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<RoomHub>("/messageHub");

app.Run();