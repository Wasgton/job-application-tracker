using JobApplicationTracker.Application.Exception;
using JobApplicationTracker.Application.Repository;

namespace JobApplicationTracker.Application.UseCases.JobUseCases;

public class UnarchiveJobUseCase(IJobRepository repository)
{
    private readonly IJobRepository _repository = repository;

    public void Execute(string id)
    {
        var job = _repository.GetById(id);
        if (job == null) throw new NotFoundException("Job not found");
        job.UnArchive();
        _repository.Update(job);
    }
}