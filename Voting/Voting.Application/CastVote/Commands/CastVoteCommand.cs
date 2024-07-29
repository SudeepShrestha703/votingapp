using MediatR;
using Voting.Application.Interfaces;
using Voting.Domain.Entities;

namespace Voting.Application.CastVote.Commands
{
    public class CastVoteHandler : IRequestHandler<CastVoteCommand, Unit>
    {
        private readonly IVotingDbContext _context;
        public CastVoteHandler(IVotingDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CastVoteCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _context.Candidates.FindAsync(request.CandidateId);
            var voter = await _context.Voters.FindAsync(request.VoterId);

            if (candidate == null || voter is null || voter.HasVoted)
            {
                throw new Exception("Invalid voting");
            }

            var vote = new Vote
            {
                CandidateId = request.CandidateId,
                VoterId = request.VoterId,
            };

            candidate.Votes += 1;
            voter.HasVoted = true;

            _context.Votes.Add(vote);
            await _context.SaveChangesAsync(cancellationToken);

            //return vote.Id;
            return Unit.Value;
        }
    }

    public class CastVoteCommand : IRequest<Unit>
    {
        public int VoterId { get; set; }
        public int CandidateId { get; set; }
    }
}
