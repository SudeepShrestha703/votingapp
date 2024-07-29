namespace Voting.Domain.Entities
{
    public class Candidate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Votes { get; set; } = 0;
        public virtual ICollection<Vote> VoteRef { get; }
    }
}
