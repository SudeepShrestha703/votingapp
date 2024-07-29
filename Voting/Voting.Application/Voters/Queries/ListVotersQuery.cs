using MediatR;
using Microsoft.EntityFrameworkCore;
using Voting.Application.Interfaces;

namespace Voting.Application.Voters.Queries
{

    public class ListVotersHandler : IRequestHandler<ListVotersQuery, List<ListVotersResponse>>
    {
        private readonly IVotingDbContext _context;

        public ListVotersHandler(IVotingDbContext context)
        {
            _context = context;
        }

        public async Task<List<ListVotersResponse>> Handle(ListVotersQuery request, CancellationToken cancellationToken)
        {
            var response = await _context.Voters
                                       .Select(x => new ListVotersResponse
                                       {
                                           HasVoted = x.HasVoted,
                                           Id = x.Id,
                                           Name = x.Name
                                       }).ToListAsync(cancellationToken);
            return response;
        }
    }
    public class ListVotersQuery : IRequest<List<ListVotersResponse>> { }

    public class ListVotersResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasVoted { get; set; }
    }
}
