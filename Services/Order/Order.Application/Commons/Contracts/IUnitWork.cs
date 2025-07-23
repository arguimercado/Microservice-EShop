namespace Order.Application.Commons.Contracts;

public interface IUnitWork
{
    Task<int> CommitSaveChangesAsync(CancellationToken cancellationToken = default);
}
