using Commons.Domain.Communication;
using Commons.Domain.Data;

namespace Commons.Domain.Messages;

public abstract class CommandHandler
{
    protected ValidationResult ValidationResult;

    protected CommandHandler()
    {
        ValidationResult = new ValidationResult();
    }

    protected void AddError(Error error)
    {
        ValidationResult.AddError(error);
    }

    protected async Task<ValidationResult> PersistData(IUnitOfWork uow)
    {
        if (!await uow.Commit()) AddError(Error.Commit);
        return ValidationResult;
    }
}