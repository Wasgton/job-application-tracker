using JobApplicationTracker.Domain.Entity.Job;
using JobApplicationTracker.domain.repository;
using JobApplicationTracker.Infra.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace JobApplicationTracker.infra.database.Sqlserver;

public class MssqlEntityJobRepository : IJobRepository
{
    private readonly DbContext _context = new EntityDbContext();
    
    public List<Job> GetAll()
    {
        return _context.Set<Job>().ToList();
    }

    public Job? GetById(string id)
    {
        return _context.Find<Job>(id);
    }

    public Guid Create(Job job)
    {
        _context.Add(job);
        _context.SaveChanges();
        if(!job.Id.HasValue) throw new Exception("Job not found");
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

}