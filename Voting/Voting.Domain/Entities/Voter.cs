namespace Voting.Domain.Entities
{
    public class Voter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasVoted { get; set; } = false;
        public virtual ICollection<Vote> VoteRef { get; }
    }
}
