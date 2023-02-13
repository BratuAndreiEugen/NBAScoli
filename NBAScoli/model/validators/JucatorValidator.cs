using System.ComponentModel.DataAnnotations;

namespace NBAScoli.model.validators;

public class JucatorValidator : IValidator<Jucator>
{
    public void Validate(Jucator e)
    {
        if (e.Nume.Length > 100)
            throw new ValidationException("Jucatorul nu este valid");
    }
}