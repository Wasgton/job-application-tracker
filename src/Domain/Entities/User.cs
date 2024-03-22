using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobApplicationTracker.Domain.Entities;

public class User
{
    [Column("id")]
    public Guid? Id { get; set; }
    [Column("username")]
    public string Username { get; set; }
    [Column("hash")]
    public string Hash { get; set; }
    [Column("salt")]
    public string? Salt { get; set; }

    public User(){}
    public User(string username, string hash, string? salt)
    {
        Username = username;
        Hash = hash;
        Salt = salt;
        Validation();
    }
    
    public void Validation()
    {
        if (string.IsNullOrEmpty(Username)) throw new ValidationException("Username is required");
        if (string.IsNullOrEmpty(Hash)) throw new ValidationException("Password is required");
        if (string.IsNullOrEmpty(Salt)) throw new ValidationException("Salt is required");
    }
}