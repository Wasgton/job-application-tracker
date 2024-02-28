namespace JobApplicationTracker.Application.Exception;

public abstract class CustomException : System.Exception
{
    protected readonly string CustomMessage;
    protected CustomException(string message)
    {
        CustomMessage = message;
    }
}