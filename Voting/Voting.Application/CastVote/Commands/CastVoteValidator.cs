using FluentValidation;

namespace Voting.Application.CastVote.Commands
{
    public class CastVoteValidator : AbstractValidator<CastVoteCommand>
    {
        public CastVoteValidator()
        {
            RuleFor(x => x.CandidateId)
                .GreaterThan(0).WithMessage("CandidateId is required.");

            RuleFor(x => x.VoterId)
                .GreaterThan(0).WithMessage("VoterId is required.");
        }
    }
}
