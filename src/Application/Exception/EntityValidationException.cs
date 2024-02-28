using System.Transactions;

namespace JobApplicationTracker.Application.Exception;

public class EntityValidationException : CustomException
{

    public EntityValidationException(string message) : base(message){}
}