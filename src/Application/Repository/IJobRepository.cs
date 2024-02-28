using JobApplicationTracker.Domain.Entity.Job;

namespace JobApplicationTracker.domain.repository;

public interface IJobRepository
{
    public List<Job> GetAll();
    public Job? GetById(string id);
    public Guid Create(Job job);
    public void Update(Job job);
    public void Delete(Job job);
}