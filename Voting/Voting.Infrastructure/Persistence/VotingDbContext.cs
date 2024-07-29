using Microsoft.EntityFrameworkCore;
using Voting.Application.Interfaces;
using Voting.Domain.Entities;

namespace Voting.Infrastructure.Persistence
{
    public class VotingDbContext : DbContext, IVotingDbContext
    {
        public VotingDbContext(DbContextOptions<VotingDbContext> options) : base(options) { }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Voter> Voters { get; set; }
        public DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Candidate entity
            modelBuilder.Entity<Candidate>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(e => e.Votes)
                      .IsRequired()
                      .HasDefaultValue(0);
                entity.HasMany(e => e.VoteRef)
                      .WithOne(v => v.Candidate)
                      .HasForeignKey(v => v.CandidateId);
            });

            // Configure Voter entity
            modelBuilder.Entity<Voter>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(e => e.HasVoted)
                      .IsRequired()
                      .HasDefaultValue(false);
                entity.HasMany(e => e.VoteRef)
                      .WithOne(v => v.Voter)
                      .HasForeignKey(v => v.VoterId);
            });

            // Configure Vote entity
            modelBuilder.Entity<Vote>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Candidate)
                      .WithMany(c => c.VoteRef)
                      .HasForeignKey(e => e.CandidateId)
                      .IsRequired();
                entity.HasOne(e => e.Voter)
                      .WithMany(v => v.VoteRef)
                      .HasForeignKey(e => e.VoterId)
                      .IsRequired();
            });

        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
