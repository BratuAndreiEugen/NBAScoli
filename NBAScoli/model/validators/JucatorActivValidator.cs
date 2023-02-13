using System.ComponentModel.DataAnnotations;

namespace NBAScoli.model.validators;

public class JucatorActivValidator : IValidator<JucatorActiv>
{
    public void Validate(JucatorActiv e)
    {
        if (e.Puncte > 50)
            throw new ValidationException("Jucatorul are prea multe puncte");
    }
}