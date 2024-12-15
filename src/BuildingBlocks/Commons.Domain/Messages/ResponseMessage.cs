using Commons.Domain.Communication;

namespace Commons.Domain.Messages;

public class ResponseMessage : Message
{
    public ResponseMessage(ValidationResult validationResult)
    {
        ValidationResult = validationResult;
    }

    public ValidationResult ValidationResult { get; set; }
}