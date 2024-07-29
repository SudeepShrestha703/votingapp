using MediatR;
using Microsoft.EntityFrameworkCore;
using Voting.Application.Interfaces;

namespace Voting.Application.Candidates.Queries
{
    public class ListCandidatesQueryHandler : IRequestHandler<ListCandidatesQuery, IList<ListCandidatesResponse>>
    {
        private readonly IVotingDbContext _context;
        public ListCandidatesQueryHandler(IVotingDbContext context)
        {
            _context = context;
        }

        public async Task<IList<ListCandidatesResponse>> Handle(ListCandidatesQuery request, CancellationToken cancellationToken)
        {
            var response = await _context.Candidates
                                   .Select(x => new ListCandidatesResponse
                                   {
                                       Id = x.Id,
                                       Name = x.Name,
                                       Votes = x.Votes

                                   }).ToListAsync(cancellationToken);
            return response;
        }
    }

    public class ListCandidatesQuery : IRequest<IList<ListCandidatesResponse>> { }

    public class ListCandidatesResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Votes { get; set; }
    }
}
