using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PraOnde.API.Domain.Entities;

public class Message : BaseEntity
{
    public Message()
    {
        
    }

    public Message(Guid userId, Guid roomId, string content)
    {
        SetUserId(userId);
        SetRoomId(roomId);  
        SetContent(content);
        
    }
    [ForeignKey("userId")]
    public Guid UserId { get; set; }
    
    [ForeignKey("roomId")]
    public Guid RoomId { get; set; }
    public required string Content  { get; set; }

    public Message SetUserId(Guid userId)
    {
        UserId = userId;
        return this;
    }

    public Message SetRoomId(Guid roomId)
    {
        RoomId = roomId;
        return this;
    }

    public Message SetContent(string content)
    {
        Content = content;
        return this;
    }
}