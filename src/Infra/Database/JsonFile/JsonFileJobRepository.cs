using System.Text.Json;
using JobApplicationTracker.Application.Repository;
using JobApplicationTracker.Domain.Entities;

namespace JobApplicationTracker.Infra.Database.JsonFile;

public class JsonFileJobRepository : IJobRepository
{
    private readonly string _jsonFile = "db.json";
    private readonly JsonSerializerOptions _options = new();

    public JsonFileJobRepository()
    {
        _options.PropertyNameCaseInsensitive = true;
    }

    public List<Job> GetAll()
    {
        var json = File.ReadAllText(_jsonFile);
        var jobs = JsonSerializer.Deserialize<IEnumerable<Job>>(json, _options);
        if (jobs == null) return new List<Job>();
        return jobs.ToList();
    }

    public Job? GetById(string id)
    {
        var jobs = GetAll();
        var idGuid = Guid.Parse(id);
        var job = jobs.Where(x=> x.DeletedAt == null).FirstOrDefault(x => x.Id == idGuid);
        return job;
    }
    
    public Job? GetDeletedById(string id)
    {
        var jobs = GetAll();
        var idGuid = Guid.Parse(id);
        var job = jobs.Where(x=> x.DeletedAt != null).FirstOrDefault(x => x.Id == idGuid);
        return job;
    }

    public Guid Create(Job job)
    {
        var jobs = GetAll();
        job.GenerateId();
        jobs.Add(job);
        var jobsSerialized = JsonSerializer.Serialize(jobs);
        File.WriteAllText(_jsonFile, jobsSerialized);
        return (Guid)job.Id!;
    }

    public void Update(Job job)
    {
        throw new NotImplementedException();
    }

    public void Delete(Job job)
    {
        var jobs = GetAll();
        var newJobs = jobs.Where(x => x.Id != job.Id).ToList();
        var jobsSerialized = JsonSerializer.Serialize(newJobs);
        File.WriteAllText(_jsonFile, jobsSerialized);
    }

    public List<Job> GetAllDeleted()
    {
        var jobs = GetAll();
        return jobs.Where(x => x.DeletedAt != null).ToList();
    }
}