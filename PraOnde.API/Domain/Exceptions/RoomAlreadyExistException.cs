using System.Runtime.Serialization;

namespace PraOnde.API.Domain.Exceptions;

[Serializable]
public class RoomAlreadyExistException : Exception
{
    public RoomAlreadyExistException() : base("Room already exist") {}
    
    public RoomAlreadyExistException(string message) : base(message) {}
    
    public RoomAlreadyExistException(string message, Exception innerException) 
        : base(message, innerException) { }

    protected RoomAlreadyExistException(SerializationInfo info, StreamingContext context) 
        : base(info, context) { }

}