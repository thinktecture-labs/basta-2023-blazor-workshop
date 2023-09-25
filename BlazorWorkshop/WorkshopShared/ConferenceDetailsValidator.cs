using FluentValidation;

namespace WorkshopShared;

public class ConferenceDetailsValidator : AbstractValidator<ConferenceDetails>
{
    public ConferenceDetailsValidator()
    {
        RuleFor(conference =>
            conference.DateTo).GreaterThanOrEqualTo(conference => conference.DateFrom)
            .WithMessage("Enddatum muss nach Startdatum liegen");
    }
}
