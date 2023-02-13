using System.ComponentModel.DataAnnotations;

namespace NBAScoli.model.validators;

public class MeciValidator : IValidator<Meci>
{
    public void Validate(Meci e)
    {
        if (e.Data.Year > 2023)
            throw new ValidationException("Nu putem prelucra meciuri care nu au avut loc");
    }
}