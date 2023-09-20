using FluentValidation;
using MediatRDemo.Application.Commands;

namespace MediatRDemo.Application.Validation;

public class CreateMovieValidator : AbstractValidator<CreateMovieCommand>
{
    public CreateMovieValidator()
    {
        RuleFor(command => command.Movie.Title)
            .Must(StartWithUpperCase)
            .WithMessage("The title must start with an uppercase letter.");

        RuleFor(command => command.Movie.ReleaseYear)
            .GreaterThanOrEqualTo(1900)
            .WithMessage("The release year must be greater or equal to 1900.")
            .LessThanOrEqualTo(DateTime.UtcNow.Year)
            .WithMessage("The release year must be less or equal to the current year.");
    }

    private bool StartWithUpperCase(string title)
    {
        if (string.IsNullOrEmpty(title))
        {
            return false;
        }

        return char.IsUpper(title[0]);
    }
}
