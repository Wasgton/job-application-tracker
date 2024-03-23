namespace JobApplicationTracker.Application.Exception;

public class LoginException : CustomException
{
    public LoginException(string message) : base(message){}
}