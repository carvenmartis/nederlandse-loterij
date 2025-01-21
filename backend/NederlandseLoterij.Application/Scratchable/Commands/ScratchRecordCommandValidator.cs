using FluentValidation;

namespace NederlandseLoterij.Application.Scratchable.Commands;

/// <summary>
/// Validator for the ScratchRecordCommand.
/// </summary>
public class ScratchRecordCommandValidator : AbstractValidator<ScratchRecordCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ScratchRecordCommandValidator"/> class.
    /// </summary>
    public ScratchRecordCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("UserId cannot be an empty GUID.");

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("UserId is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("UserId cannot be an empty GUID.");
    }
}