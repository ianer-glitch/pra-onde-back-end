using System.ComponentModel.DataAnnotations.Schema;

namespace PraOnde.API.Domain.Entities;

public class UserRoom : BaseEntity
{
    public UserRoom()
    {
        
    }

    public UserRoom(Guid userId, Guid roomId)
    {
        UserId = userId;    
        RoomId = roomId;    
    }
    
    [ForeignKey("userId")]
    public Guid UserId { get; set; }
    
    [ForeignKey("roomId")]
    public Guid RoomId { get; set; } 
    

    public UserRoom SetUserId(Guid userId)
    {
        UserId = userId;
        return this;
    }

    public UserRoom SetRoomId(Guid roomId)
    {
        RoomId = roomId;
        return this;
    }
}