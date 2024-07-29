namespace Voting.Domain.Entities
{
    public class Vote
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public int VoterId { get; set; }
        public virtual Candidate Candidate { get; set; }
        public virtual Voter Voter { get; set; }
    }
}
