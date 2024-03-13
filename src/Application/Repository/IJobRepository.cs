using JobApplicationTracker.Domain.Entities;

namespace JobApplicationTracker.Application.Repository;

public interface IJobRepository
{
    public List<Job> GetAll();
    public Job? GetById(string id);
    public Guid Create(Job job);
    public void Update(Job job);
    public void Delete(Job job);
    public Job? GetDeletedById(string id);
    public List<Job> GetAllDeleted();
}