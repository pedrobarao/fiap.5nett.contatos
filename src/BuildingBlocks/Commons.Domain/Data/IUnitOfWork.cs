namespace Commons.Domain.Data;

public interface IUnitOfWork
{
    Task<bool> Commit();
}