using JobApplicationTracker.Application.Exception;
using JobApplicationTracker.Application.Repository;

namespace JobApplicationTracker.Application.UseCases.JobUseCases;

public class RestoreJobUseCase(IJobRepository repository)
{
    private readonly IJobRepository _repository = repository;

    public void Execute(string id)
    {
        var job = _repository.GetDeletedById(id);
        if (job == null) throw new NotFoundException("Job not found");
        job.Restore();
        Console.WriteLine(job);
        _repository.Update(job);
    }
}