using JobApplicationTracker.Application.Repository;
using JobApplicationTracker.Domain.Entities;
using JobApplicationTracker.Infra.Database.Contexts;

namespace JobApplicationTracker.infra.database.Sqlserver;

public class MssqlEntityJobRepository : IJobRepository
{
    private readonly EntityDbContext _context = new();

    public List<Job> GetAll()
    {
        return _context.Set<Job>().ToList();
    }

    public Job? GetById(string id)
    {
        return _context.Jobs!
            .Where(x => x.DeletedAt == null)
            .First(x => x.Id == Guid.Parse(id));
    }

    public Job? GetDeletedById(string id)
    {
        return _context.Jobs!
            .Where(x => x.DeletedAt != null)
            .First(x => x.Id == Guid.Parse(id));
    }

    public Guid Create(Job job)
    {
        _context.Jobs!.Add(job);
        _context.SaveChanges();
        if (!job.Id.HasValue) throw new Exception("Job not found");
        return job.Id.Value;
    }

    public void Update(Job job)
    {
        _context.Update(job);
        _context.SaveChanges();
    }

    public void Delete(Job job)
    {
        if (job == null) throw new Exception("Job not found");
        _context.Remove(job);
        _context.SaveChanges();
    }

    public List<Job> GetAllDeleted()
    {
        return _context.Jobs!
            .Where(job =>  job.DeletedAt != null ).ToList();
    }
}