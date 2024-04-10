using JobApplicationTracker.Application.Exception;
using JobApplicationTracker.Application.Repository;
using JobApplicationTracker.Domain.Entities;
using JobApplicationTracker.Infra.Database.Contexts;

namespace JobApplicationTracker.infra.database.Sqlserver;

public class MssqlEntityUserRepository : IUserRepository
{
    private readonly EntityDbContext _context = new();

    public User? GetById(Guid id)
    {
        return _context.Users!
            .First(user => user.Id == id);
    }

    public User? GetByUserName(string username)
    {
        return _context.Users!
            .FirstOrDefault(user => user.Username == username);
    }

    public List<User> GetAll()
    {
        return _context.Users!.ToList();
    }

    public Guid Create(User user)
    {
        _context.Add(user);
        _context.SaveChanges();
        if (!user.Id.HasValue) throw new NotFoundException("User not found");
        return user.Id.Value;
    }

    public void Update(User user)
    {
        throw new NotImplementedException();
    }

    public void Delete(User user)
    {
        _context.Remove(user);
        _context.SaveChanges();
    }

    
    
}