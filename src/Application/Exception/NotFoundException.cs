namespace JobApplicationTracker.Application.Exception;

public class NotFoundException : CustomException
{
    public NotFoundException(string message) : base(message){}
}