namespace JobApplicationTracker.Application.Dto;

public class JWTOutput
{
    public required string Token { get; set; }
    public required int ExpireIn { get; set; }
}