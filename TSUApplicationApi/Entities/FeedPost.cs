namespace TSUApplicationApi.Entities
{
    public class FeedPost
    {

        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Guid UserId { get; set; }
        public User User { get; set; }

        public ICollection<FeedComment> Comments { get; set; } = new List<FeedComment>();
    }
}
