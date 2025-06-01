namespace TSUApplicationApi.Entities
{
    public class FeedComment
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int FeedPostId { get; set; }
        public FeedPost FeedPost { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
