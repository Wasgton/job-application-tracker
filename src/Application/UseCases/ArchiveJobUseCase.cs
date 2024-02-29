using JobApplicationTracker.Application.Exception;
using JobApplicationTracker.domain.repository;

namespace JobApplicationTracker.Application.UseCases;

public class ArchiveJobUseCase(IJobRepository repository)
{
    private readonly IJobRepository _repository = repository;

    public void Execute(string id)
    {
        var job = _repository.GetById(id);
        if (job == null) throw new NotFoundException("Job not found");
        job.Archive();
        _repository.Update(job);
    }
}