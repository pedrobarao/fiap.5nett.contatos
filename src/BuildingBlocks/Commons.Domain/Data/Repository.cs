using Commons.Domain.DomainObjects;

namespace Commons.Domain.Data;

public interface IRepository<T> : IDisposable where T : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
}