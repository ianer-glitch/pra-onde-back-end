namespace PraOnde.API.Domain.Entities;

public class User : BaseEntity
{
    public User()
    {
        
    }

    public User(string name)
    {
        SetName(name);
    }
    public string Name { get; set; }    
    

    public User SetName(string name)
    {
        Name = name;
        return this;
    }
  
}