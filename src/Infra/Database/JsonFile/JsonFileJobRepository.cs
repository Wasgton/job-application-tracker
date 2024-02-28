using System.Collections;
using System.Text.Json;
using JobApplicationTracker.Domain.Entity.Job;
using JobApplicationTracker.domain.repository;

namespace JobApplicationTracker.Infra.Database.JsonFile;

public class JsonFileJobRepository : IJobRepository
{
    private string _jsonFile = "db.json";
    private JsonSerializerOptions _options = new();
    
    public JsonFileJobRepository()
    {
       _options.PropertyNameCaseInsensitive = true;
    }
    
    public List<Job> GetAll()
    {
        string json = File.ReadAllText(_jsonFile);
        IEnumerable<Job>? jobs = JsonSerializer.Deserialize<IEnumerable<Job>>(json, _options);
        if (jobs == null) return new List<Job>();
        return jobs.ToList(); 
    }

    public Job? GetById(string id)
    {
        List<Job> jobs = GetAll();
        Guid idGuid = Guid.Parse(id);
        Job? job = jobs.FirstOrDefault(x => x.Id == idGuid);
        return job;
    }
    
    public Guid Create(Job job)
    {
        List<Job> jobs = GetAll();
        job.GenerateId();
        jobs.Add(job);
        string jobsSerialized = JsonSerializer.Serialize(jobs);
        File.WriteAllText(_jsonFile, jobsSerialized);
        return (Guid) job.Id!;
    }

    public void Update(Job job)
    {
        throw new NotImplementedException();
    }

    public void Delete(Job job)
    {
        List<Job> jobs = GetAll();
        List<Job> newJobs = jobs.Where(x => x.Id != job.Id).ToList();
        string jobsSerialized = JsonSerializer.Serialize(newJobs);
        File.WriteAllText(_jsonFile, jobsSerialized);
    }
}