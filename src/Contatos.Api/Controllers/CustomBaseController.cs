using Microsoft.AspNetCore.Mvc;

namespace Contatos.Api.Controllers;

[ApiController]
public abstract class CustomBaseController : ControllerBase
{
    protected ICollection<string> Errors = new List<string>();

    protected ActionResult Respond(ObjectResult? result = null)
    {
        if (!IsValidOperation())
            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "details", Errors.ToArray() }
            }));

        if (result is null) return NoContent();

        return result;
    }

    private bool IsValidOperation()
    {
        return !Errors.Any();
    }

    protected void AddError(string erro)
    {
        Errors.Add(erro);
    }

    protected void AddErrors(ICollection<string> errors)
    {
        foreach (var error in errors) AddError(error);
    }
}