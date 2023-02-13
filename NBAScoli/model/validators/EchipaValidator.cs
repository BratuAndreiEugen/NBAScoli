using System.ComponentModel.DataAnnotations;

namespace NBAScoli.model.validators;

public class EchipaValidator : IValidator<Echipa>
{
    public void Validate(Echipa e)
    {
        if (e.Nume.Length > 100)
            throw new ValidationException("Echipa nu este valid");
    }
}
