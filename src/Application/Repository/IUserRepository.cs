using JobApplicationTracker.Domain.Entities;

namespace JobApplicationTracker.Application.Repository;

public interface IUserRepository
{
    public User? GetById(Guid id);
    public User? GetByUserName(string username);
    public List<User> GetAll();
    public Guid Create(User user);
    public void Update(User user);
    public void Delete(User user);
}