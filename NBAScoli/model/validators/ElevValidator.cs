using System.ComponentModel.DataAnnotations;

namespace NBAScoli.model.validators;

public class ElevValidator : IValidator<Elev>
{
    public void Validate(Elev e)
    {
        if (e.Nume.Length > 100)
            throw new ValidationException("Elevul nu este valid");
    }
}