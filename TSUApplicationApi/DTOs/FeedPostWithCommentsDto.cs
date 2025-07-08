using System.Text.Json.Serialization;

namespace TSUApplicationApi.DTOs
{
    public class FeedPostWithCommentsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? IsApproved { get; set; }
        public List<FeedCommentDto> Comments { get; set; } = new();
    }
}
