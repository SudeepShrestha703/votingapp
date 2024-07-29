using FluentValidation;

namespace Voting.Application.Voters.Commands
{
    public class CreateVoterValidator : AbstractValidator<CreateVoterCommand>
    {
        public CreateVoterValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(2, 100).WithMessage("Name must be between 2 and 100 characters.");
        }
    }
}
