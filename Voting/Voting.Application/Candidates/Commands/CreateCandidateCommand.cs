using MediatR;
using Voting.Application.Interfaces;
using Voting.Domain.Entities;

namespace Voting.Application.Candidates.Commands
{
    public class CreateCandidateHandler : IRequestHandler<CreateCandidateCommand, int>
    {
        private readonly IVotingDbContext _context;
        public CreateCandidateHandler(IVotingDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidate = new Candidate
            {
                Name = request.Name,
            };

            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync(cancellationToken);

            return candidate.Id;
        }
    }

    public class CreateCandidateCommand : IRequest<int>
    {
        public string Name { get; set; }
    }
}
