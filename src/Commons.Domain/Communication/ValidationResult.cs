namespace Commons.Domain.Communication;

public class ValidationResult
{
    public List<string> Errors { get; } = new();
    public bool IsValid => !Errors.Any();
}