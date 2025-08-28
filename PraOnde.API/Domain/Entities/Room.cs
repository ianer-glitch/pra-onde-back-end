namespace PraOnde.API.Domain.Entities;

public class Room : BaseEntity
{
    public Room()
    {
        
    }

    public Room(string name)
    {
        
    }
    
    public string Name { get; set; }

    public Room SetName(string name)
    {
        Name = name;    
        return this;
    }
}