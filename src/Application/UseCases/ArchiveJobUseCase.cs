using JobApplicationTracker.Application.Exception;
using JobApplicationTracker.Domain.Entity.Job;
using JobApplicationTracker.domain.repository;
using JobApplicationTracker.infra.database.Sqlserver;

namespace JobApplicationTracker.Application.UseCases;

public class ArchiveJobUseCase(IJobRepository? repository = null)
{
    private IJobRepository _repository = repository?? new MssqlDapperJobRepository();

    public void Execute(string id)
    {
        Job? job = _repository.GetById(id);
        if (job == null) throw new NotFoundException("Job not found");
        job.Archive();
        _repository.Update(job);
    }
}