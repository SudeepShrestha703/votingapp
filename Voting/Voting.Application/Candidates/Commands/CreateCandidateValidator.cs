using FluentValidation;

namespace Voting.Application.Candidates.Commands
{
    public class CreateCandidateValidator : AbstractValidator<CreateCandidateCommand>
    {
        public CreateCandidateValidator()
        {
            RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Name is required.")
           .Length(2, 100).WithMessage("Name must be between 2 and 100 characters.");
        }
    }
}
