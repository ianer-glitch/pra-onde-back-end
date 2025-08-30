namespace PraOnde.API.Domain.Exceptions;
[Serializable]
public class UserAlreadyExistException :Exception
{
    public UserAlreadyExistException(string username) : base($"An user with name {username} already exists.")
    {
    }


}