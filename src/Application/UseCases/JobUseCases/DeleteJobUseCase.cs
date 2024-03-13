using JobApplicationTracker.Application.Exception;
using JobApplicationTracker.Application.Repository;

namespace JobApplicationTracker.Application.UseCases.JobUseCases;

public class DeleteJobUseCase
{
    private readonly IJobRepository _repository;

    public DeleteJobUseCase(IJobRepository repository)
    {
        _repository = repository;
    }

    public void Execute(string id)
    {
        var job = _repository.GetById(id);
        if (job == null) throw new NotFoundException("Job not found");
        _repository.Delete(job);
    }
}