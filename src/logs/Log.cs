namespace JobApplicationTracker.logs;

public class Log
{
    public static void info(string message)
    {
        string messageToAppend = @$"Trace: {message}";
        File.AppendAllText("./src/logs/log.txt", $"\n {messageToAppend}\n");
    }
}