using MediatR;
using Voting.Application.Interfaces;
using Voting.Domain.Entities;

namespace Voting.Application.Voters.Commands
{
    public class CreateVoterHandler : IRequestHandler<CreateVoterCommand, int>
    {
        private readonly IVotingDbContext _context;
        public CreateVoterHandler(IVotingDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateVoterCommand request, CancellationToken cancellationToken)
        {
            var dbVoter = new Voter
            {
                Name = request.Name,
            };
           
            _context.Voters.Add(dbVoter);
            await _context.SaveChangesAsync(cancellationToken);
           
            return dbVoter.Id;
        }
    }

    public class CreateVoterCommand : IRequest<int>
    {
        public string Name { get; set; }
    }
}
