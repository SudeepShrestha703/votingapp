using Microsoft.EntityFrameworkCore;
using Voting.Domain.Entities;

namespace Voting.Application.Interfaces
{
    public interface IVotingDbContext
    {
        DbSet<Candidate> Candidates { get; set; }
        DbSet<Voter> Voters { get; set; }
        DbSet<Vote> Votes { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
