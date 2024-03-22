namespace JobApplicationTracker.Application.Exception;

public class AlreadyRegisteredException : CustomException
{
    public AlreadyRegisteredException(string message) : base(message){}
}