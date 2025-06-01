namespace TSUApplicationApi.DTOs
{
    public class FeedPostWithCommentsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public List<FeedCommentDto> Comments { get; set; } = new();
    }
}
